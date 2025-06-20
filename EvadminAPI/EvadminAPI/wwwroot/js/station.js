document.addEventListener('DOMContentLoaded', () => {


    async function fetchJson(url) {
        const res = await fetch(url, { credentials: 'same-origin' });
        if (!res.ok) throw new Error(`${res.status} ${res.statusText}`);
        return res.json();
    }

    async function loadData() {
        try {

            const m = await fetchJson('/Manager/metrics');

            totalStations.textContent = m.totalStations;
            activeStations.textContent = m.activeStations;
            availableStations.textContent = m.availableStations;
            unavailableStations.textContent = m.unavailableStations;
           
        } catch (err) {
            console.error('Dashboard:', err);
        }
    }

    async function renderStations() {
        const stations = await fetchJson('/Station');
        const container = document.getElementById('stationsContainer');
        container.innerHTML = '';

        stations.forEach(station => {
            container.insertAdjacentHTML('beforeend', `
            <div class="col-xxl-3 col-xl-6 col-lg-6 col-12">
              <div class="box box-body pull-up station-card"
                    data-lat="${station.latitude}"
                    data-lng="${station.longitude}">
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

        document.querySelectorAll('.station-card').forEach(card => {
            card.addEventListener('click', function () {
                const lat = this.dataset.lat;
                const lng = this.dataset.lng;

                console.log(`Clicked station: ${lat}, ${lng}`); 

                const mapFrame = document.getElementById('stationMap');
                mapFrame.style.display = 'block';

                mapFrame.src =`https://maps.google.com/maps?q=${lat},${lng}&output=embed`;
            });
        });
    }

    loadData()
    renderStations()
});
