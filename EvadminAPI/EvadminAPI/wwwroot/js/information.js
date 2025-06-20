document.addEventListener('DOMContentLoaded', () => {

    const tableBody = document.getElementById('sessionsTableBody');

    async function fetchJson(url) {
        const res = await fetch(url, { credentials: 'same-origin' });
        if (!res.ok) throw new Error(`${res.status} ${res.statusText}`);
        return res.json();
    }

    async function loadOwnerData() {
        try {
            // Загрузка метрик владельца
            const metrics = await fetchJson('/Owner/metrics');

            document.getElementById('ownerTotalStations').textContent = metrics.totalStations;
            document.getElementById('activeSessions').textContent = metrics.activeSessions;
            document.getElementById('inactiveSessions').textContent = metrics.inactiveSessions;
            document.getElementById('totalEnergy').textContent = metrics.totalEnergy.toFixed(0);
            document.getElementById('totalRevenue').textContent = metrics.totalRevenue.toFixed(0);

            // Загрузка станций владельца
            const stations = await fetchJson('/Station');
            renderStations(stations);

            // Загрузка сессий владельца
            const sessions = await fetchJson('/Session/OwnerSessions');
            renderSessions(sessions);

        } catch (err) {
            console.error('Owner data loading failed:', err);
        }
    }

    function renderStations(stations) {
        const container = document.getElementById('stationsContainer');
        container.innerHTML = '';

        stations.forEach(station => {
            container.insertAdjacentHTML('beforeend', `
            <div class="col-xxl-3 col-xl-6 col-lg-6 col-12">
                <div class="box box-body pull-up">
                    <div class="d-flex justify-content-between align-items-center">
                        <img src="../../../images/charging-station.png" class="w-50 h-50">
                    </div>
                    <div class="d-flex justify-content-between align-items-center mt-10">
                        <h1 class="mb-0">${station.distance}<span class="text-fade fs-16"> miles</span></h1>
                        </span>
                    </div>
                    <div>
                        <h4 class="mt-10">${station.name}</h4>
                    </div>
                    <div class="d-flex justify-content-between align-items-center">
                        <p class="text-fade mb-0">Type</p>
                        <p class="text-fade mb-0">Price</p>
                        <p class="text-fade mb-0">Slot</p>
                    </div>
                    <div class="d-flex justify-content-between align-items-center">
                        <h5 class="mb-0">${station.type}</h5>
                        <h5 class="mb-0">$${station.price}/kW</h5>
                        <h5 class="mb-0">${station.slot}</h5>
                    </div>
                </div>
            </div>
        `);
        });
    }

    async function loadSessions() {
        try {
            // ✔️ правильный маршрут
            const sessions = await fetchJson('/Session/OwnerSessions');

            tableBody.innerHTML = '';
            sessions.forEach(s => {
                tableBody.insertAdjacentHTML('beforeend', `
          <tr>
            <td>${s.number}</td>
            <td>${new Date(s.date).toLocaleDateString()}</td>
            <td>${s.client_name}</td>
            <td><i class="fa-solid fa-map-pin text-danger"></i> ${s.location}</td>
            <td>${s.info ?? ''}</td>
            <td>${s.energy} kWh</td>
            <td>${s.cost.toLocaleString('en-US', { style: 'currency', currency: 'USD' })}</td>
            <td><span class="btn-sm ${getStatusClass(s.status)} rounded-2">${s.status}</span></td>
          </tr>`);
            });
        } catch (err) {
            console.error('Invoices:', err);
            tableBody.innerHTML = '<tr><td colspan="8" class="text-center text-danger">Failed to load</td></tr>';
        }
    }

    /* ---------- helpers ---------- */
    function getStatusClass(status) {
        return {
            Paid: 'bg-success-light border border-3 border-success',
            Pending: 'bg-warning-light border border-3 border-warning',
            Failed: 'bg-danger-light  border border-3 border-danger'
        }[status] ?? 'bg-secondary-light border border-3 border-secondary';
    }


    loadOwnerData();
    loadSessions();

});
