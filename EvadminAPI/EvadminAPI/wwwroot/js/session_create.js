document.addEventListener('DOMContentLoaded', () => {
    const form = document.getElementById('sessionForm');
    const alertBox = document.getElementById('alertBox');
    const submitBtn = document.getElementById('submitBtn');
    const stationSel = document.getElementById('stationSelect');

    const showAlert = (msg, type = 'success') => {
        alertBox.textContent = msg;
        alertBox.className = `alert alert-${type}`;
        alertBox.classList.remove('d-none');
    };

    // 1️⃣  Load stations for dropdown
    fetch('/Station', { credentials: 'same-origin' })
        .then(r => r.ok ? r.json() : Promise.reject(r))
        .then(stations => {
            stationSel.innerHTML = '<option value="" disabled selected hidden>Select station</option>';
            stations.forEach(s => {
                const opt = document.createElement('option');
                opt.value = s.id;
                opt.text = `${s.name} (${s.id.slice(0, 8)})`;
                stationSel.appendChild(opt);
            });
        })
        .catch(() => stationSel.innerHTML = '<option disabled selected>No stations</option>');
    // 2️⃣  Submit handler
    form.addEventListener('submit', async e => {
        e.preventDefault();
        if (!form.checkValidity()) {
            form.reportValidity();
            return;
        }

        submitBtn.disabled = true;
        const old = submitBtn.textContent;
        submitBtn.textContent = 'Saving…';

        const token = form.querySelector('input[name="__RequestVerificationToken"]')?.value || '';

        const payload = {
            Number: document.getElementById('number').value.trim(),
            Station_id: stationSel.value,
            Date: document.getElementById('startDate').value,   // yyyy-MM-dd
            Client_name: document.getElementById('clientName').value.trim(),
            Location: document.getElementById('location').value,
            Info: document.getElementById('info').value,
            Energy: +document.getElementById('energy').value,
            Cost: +document.getElementById('cost').value,
            Status: document.getElementById('status').value
        };

        try {
            const res = await fetch('/Session', {
                method: 'POST',
                credentials: 'same-origin',
                headers: {
                    'Content-Type': 'application/json',
                    ...(token && { 'RequestVerificationToken': token })
                },
                body: JSON.stringify(payload)
            });

            if (res.ok) {
                showAlert('✅ Session added');
                form.reset();
            } else {
                showAlert(await res.text() || 'Error', 'danger');
            }
        } catch (err) {
            console.error(err);
            showAlert('Network error', 'danger');
        } finally {
            submitBtn.disabled = false;
            submitBtn.textContent = old;
        }
    });
});