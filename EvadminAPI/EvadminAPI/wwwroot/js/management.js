document.addEventListener('DOMContentLoaded', () => {

    const tableBody = document.getElementById('sessionsTableBody');

    /** универсальный helper — всегда возвращает JSON или бросает ошибку */
    async function fetchJson(url) {
        const res = await fetch(url, { credentials: 'same-origin' });
        if (!res.ok) throw new Error(`${res.status} ${res.statusText}`);
        return res.json();
    }

    /* ---------- DASHBOARD ---------- */
    async function loadDashboardData() {
        try {
            // ▶️ правильный маршрут
            const m = await fetchJson('/Manager/metrics');

            totalStations.textContent = m.totalStations;
            activeStations.textContent = m.activeStations;
            totalSessions.textContent = m.totalSessions;
            totalEnergy.innerHTML = `${m.totalEnergy.toLocaleString()} <span class="fs-16">kWh</span>`;
            totalRevenue.textContent = m.totalRevenue.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
            newUsers.textContent = m.newUsers;
        } catch (err) {
            console.error('Dashboard:', err);
        }
    }

    /* ---------- Invoices ---------- */
    async function loadSessions() {
        try {
            // ✔️ правильный маршрут
            const sessions = await fetchJson('/Session/All');

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
            <td><span class="btn-sm rounded-2">${s.status}</span></td>
          </tr>`);
            });
        } catch (err) {
            console.error('Invoices:', err);
            tableBody.innerHTML = '<tr><td colspan="8" class="text-center text-danger">Failed to load</td></tr>';
        }
    }


    loadDashboardData();
    loadSessions();
});