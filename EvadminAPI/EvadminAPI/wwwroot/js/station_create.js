document.addEventListener('DOMContentLoaded', () => {
    const form = document.getElementById('stationForm');
    const alertBox = document.getElementById('alertBox');
    const submitBtn = document.getElementById('submitBtn');

    /** helper */
    const showAlert = (msg, type = 'success') => {
        alertBox.textContent = msg;
        alertBox.className = `alert alert-${type}`;
    };

    form.addEventListener('submit', async e => {
        e.preventDefault();
        if (!form.checkValidity()) {
            form.reportValidity();
            return;
        }

        submitBtn.disabled = true;
        const originalText = submitBtn.textContent;
        submitBtn.textContent = 'Saving…';

        const payload = {
            Name: document.getElementById('name').value.trim(),
            Distance: Number(document.getElementById('distance').value),
            Type: document.getElementById('type').value,
            Price: Number(document.getElementById('price').value),
            Slot: document.getElementById('slot').value,
            Status: document.getElementById('status').value,
            Latitude: document.getElementById('latitude').value,
            Longitude: document.getElementById('longitude').value
        };
        try {
            const res = await fetch('/Station', {         
                method: 'POST',
                credentials: 'same-origin',                  
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(payload)
            });

            if (res.ok) {
                showAlert('✅ Station added', 'success');
                form.reset();
            } else {
                const err = await res.text();
                showAlert(err || 'Error', 'danger');
            }
        } catch (ex) {
            console.error(ex);
            showAlert('Сеть недоступна. Попробуйте позже.', 'danger');
        } finally {
            submitBtn.disabled = false;
            submitBtn.textContent = originalText;
        }
    });
});