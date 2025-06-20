document.addEventListener('DOMContentLoaded', () => {
	const registerForm = document.getElementById('registerForm');
	const submitBtn = document.getElementById('submitBtn');
	const errorContainer = document.getElementById('errorContainer');

	// Валидация формы
	const validateForm = () => {
		let isValid = true;
		const email = document.getElementById('email').value;
		const password = document.getElementById('password').value;
		const confirmPassword = document.getElementById('confirmPassword').value;
		const fullName = document.getElementById('fullName').value;

		// Сброс предыдущих ошибок
		document.querySelectorAll('.text-danger').forEach(el => el.textContent = '');
		errorContainer.classList.add('d-none');


		// Валидация пароля
		if (!password) {
			document.getElementById('passwordError').textContent = 'Password is required';
			isValid = false;
		} else if (password.length < 6) {
			document.getElementById('passwordError').textContent = 'Password must be at least 6 characters';
			isValid = false;
		}

		// Подтверждение пароля
		if (!confirmPassword) {
			document.getElementById('confirmPasswordError').textContent = 'Please confirm password';
			isValid = false;
		} else if (password !== confirmPassword) {
			document.getElementById('confirmPasswordError').textContent = 'Passwords do not match';
			isValid = false;
		}

		// Полное имя
		if (!fullName) {
			document.getElementById('fullNameError').textContent = 'Full name is required';
			isValid = false;
		}

		return isValid;
	};

	// Обработка отправки формы
	registerForm.addEventListener('submit', async (e) => {
		e.preventDefault();

		if (!validateForm()) return;

		const originalBtnText = submitBtn.innerHTML;
		submitBtn.innerHTML = 'Loading...';
		submitBtn.disabled = true;

		const userData = {
			email: document.getElementById('email').value,
			password: document.getElementById('password').value,
			fullName: document.getElementById('fullName').value
		};

		try {
			const response = await fetch('/Account/register', {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json'
				},
				body: JSON.stringify(userData)
			});

			console.log('Register response status:', response.status);
			console.log('Sending email with data:', userData);

			// Успешный ответ (200-299)
			if (response.ok) {
				const responseData = await response.text();
				console.log('Register response data:', responseData);

				// Проверяем ответ регистрации
				if (responseData && responseData.trim() !== "") {
					// Если в ответе ошибка
					if (responseData.toLowerCase().includes('error') || responseData.toLowerCase().includes('failed')) {
						errorContainer.textContent = responseData;
						errorContainer.classList.remove('d-none');
						return;
					}
				}

				console.log('Registration successful, sending email...');
				try {
					const emailResponse = await fetch('/EmailSender/registration', {
						method: 'POST',
						headers: { 
							'Content-Type': 'application/json'
						},
						body: JSON.stringify(userData)
					});
					
					console.log('Email sending status:', emailResponse.status);
					
					if (!emailResponse.ok) {
						const emailResponseText = await emailResponse.text();
						console.log('Email sending error response:', emailResponseText);
						throw new Error(`Email sending failed with status ${emailResponse.status}: ${emailResponseText}`);
					}

					// Если всё успешно
					errorContainer.textContent = 'Регистрация прошла успешно! Письмо с данными отправлено на почту пользователя.';
					errorContainer.classList.remove('d-none');
					errorContainer.classList.remove('alert-danger');
					errorContainer.classList.add('alert-success');
					registerForm.reset();
				} catch (emailError) {
					console.error('Detailed email sending error:', emailError);
					errorContainer.textContent = `Регистрация успешна, но возникла проблема с отправкой письма: ${emailError.message}`;
					errorContainer.classList.remove('d-none');
					errorContainer.classList.remove('alert-success');
					errorContainer.classList.add('alert-warning');
				}
			}
			// Ошибка (400-599)
			else {
				const contentType = response.headers.get('content-type');
				let errorMessage = 'Registration failed. Please try again.';

				if (contentType && contentType.includes('application/json')) {
					const data = await response.json();
					errorMessage = data.message || data || errorMessage;
				} else {
					errorMessage = await response.text() || errorMessage;
				}

				errorContainer.textContent = errorMessage;
				errorContainer.classList.remove('d-none');
			}
		} catch (error) {
			console.error('Registration error:', error);
			errorContainer.textContent = 'Network error. Please try again later.';
			errorContainer.classList.remove('d-none');
		} finally {
			submitBtn.innerHTML = originalBtnText;
			submitBtn.disabled = false;
		}
	});
});
