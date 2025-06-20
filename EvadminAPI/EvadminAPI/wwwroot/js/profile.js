document.addEventListener('DOMContentLoaded', function () {
    // Элементы DOM
    const elements = {
        itemsContainer: document.getElementById('items-container'),
        pagination: document.getElementById('pagination-controls'),
        userFullName: document.getElementById('user-full-name'),
        userRole: document.getElementById('user-role'),
        userFullNameValue: document.getElementById('user-full-name-value'),
        userEmail: document.getElementById('user-email'),
        dynamicTitle: document.getElementById('dynamic-title')
    };

    // Конфигурация
    const config = {
        pageSize: 3,
        currentPage: 1,
        allItems: [] // Здесь будем хранить все загруженные данные
    };

    // Основная функция загрузки данных
    async function loadData() {
        showLoader();

        try {
            // 1. Загрузка данных пользователя
            const userResponse = await fetch('/Pages/current', {
                headers: { 'Authorization': `Bearer ${localStorage.getItem('authToken')}` }
            });

            if (!userResponse.ok) throw new Error('User data error');
            const userData = await userResponse.json();

            // Сохраняем данные пользователя
            config.userId = userData.id;
            config.userRole = userData.role;

            // Обновляем UI
            updateUserProfileUI(userData);

            // 2. Загрузка контента (всех данных сразу)
            const contentEndpoint = config.userRole === 'owner'
                ? '/Station/ProfileStations'
                : '/Session/ProfileSessions';

            const contentResponse = await fetch(contentEndpoint, {
                headers: { 'Authorization': `Bearer ${localStorage.getItem('authToken')}` }
            });

            if (!contentResponse.ok) throw new Error('Content data error');
            config.allItems = await contentResponse.json();

            // Рендерим первую страницу
            renderPage(1);

        } catch (error) {
            console.error('Error:', error);
            showError();
        }
    }

    // Функция рендеринга страницы
    function renderPage(page) {
        config.currentPage = page;

        // Вычисляем элементы для текущей страницы
        const startIndex = (page - 1) * config.pageSize;
        const endIndex = startIndex + config.pageSize;
        const pageItems = config.allItems.slice(startIndex, endIndex);

        renderItems(pageItems);
        renderPagination();
    }


    // Функция обновления UI профиля пользователя
    function updateUserProfileUI(userData) {
        elements.userFullName.textContent = userData.fullName;
        elements.userRole.textContent = userData.role === 'owner' ? 'Owner' : 'Manager';
        elements.userFullNameValue.textContent = userData.fullName;
        elements.userEmail.textContent = userData.email;
        elements.dynamicTitle.textContent = userData.role === 'owner' ? 'Stations' : 'Sessions';
    }


    // Отображение элементов
    function renderItems(items) {
        if (!items || items.length === 0) {
            elements.itemsContainer.innerHTML = `
                <div class="alert alert-info text-center">
                    No ${config.userRole === 'owner' ? 'stations' : 'sessions'} found
                </div>
            `;
            return;
        }

        let html = '';
        items.forEach(item => {
            const statusClass = getStatusClass(item.status);
            const statusText = getStatusText(item.status);

            html += `
                <div class="d-flex align-items-center mb-3 p-3 border rounded" data-id="${item.id}">
                    <div class="d-flex flex-column flex-grow-1">
                        <span class="text-dark fw-bold mb-1 fs-16">${item.name}</span>
                        <span class="badge ${statusClass} align-self-start">${statusText}</span>
                    </div>
                    <div>
                        <button class="btn btn-sm btn-primary update-btn">
                            <i class="fa fa-sync-alt"></i> Update
                        </button>
                    </div>
                </div>
            `;
        });

        elements.itemsContainer.innerHTML = html;

        // Добавляем обработчики событий
        document.querySelectorAll('.update-btn').forEach(btn => {
            btn.addEventListener('click', function () {
                const itemId = this.closest('.d-flex').dataset.id;
                toggleStatus(itemId);
            });
        });
    }

    // Пагинация
    function renderPagination() {
        const totalItems = config.allItems.length;
        const totalPages = Math.ceil(totalItems / config.pageSize);

        elements.pagination.innerHTML = '';

        if (totalPages <= 1) return;

        let html = `
        <nav>
            <ul class="pagination">
                <li class="page-item ${config.currentPage === 1 ? 'disabled' : ''}">
                    <a class="page-link" href="#" data-page="prev">&laquo;</a>
                </li>
    `;

        // Адаптивный диапазон страниц
        let startPage = Math.max(1, config.currentPage - 2);
        let endPage = Math.min(totalPages, config.currentPage + 2);

        if (endPage - startPage < 4) {
            if (config.currentPage < totalPages / 2) {
                endPage = Math.min(totalPages, startPage + 4);
            } else {
                startPage = Math.max(1, endPage - 4);
            }
        }

        // Всегда показываем первую страницу
        if (startPage > 1) {
            html += `
            <li class="page-item ${config.currentPage === 1 ? 'active' : ''}">
                <a class="page-link" href="#" data-page="1">1</a>
            </li>
            ${startPage > 2 ? '<li class="page-item disabled"><span class="page-link">...</span></li>' : ''}
        `;
        }

        // Основной диапазон
        for (let i = startPage; i <= endPage; i++) {
            html += `
            <li class="page-item ${config.currentPage === i ? 'active' : ''}">
                <a class="page-link" href="#" data-page="${i}">${i}</a>
            </li>
        `;
        }

        // Всегда показываем последнюю страницу
        if (endPage < totalPages) {
            html += `
            ${endPage < totalPages - 1 ? '<li class="page-item disabled"><span class="page-link">...</span></li>' : ''}
            <li class="page-item ${config.currentPage === totalPages ? 'active' : ''}">
                <a class="page-link" href="#" data-page="${totalPages}">${totalPages}</a>
            </li>
        `;
        }

        html += `
                <li class="page-item ${config.currentPage === totalPages ? 'disabled' : ''}">
                    <a class="page-link" href="#" data-page="next">&raquo;</a>
                </li>
            </ul>
        </nav>
    `;

        elements.pagination.innerHTML = html;

        // Добавляем обработчики событий
        document.querySelectorAll('.page-link[data-page]').forEach(link => {
            link.addEventListener('click', function (e) {
                e.preventDefault();
                const targetPage = this.dataset.page;
                const totalPages = Math.ceil(config.allItems.length / config.pageSize);

                let newPage = config.currentPage;

                if (targetPage === 'prev') {
                    newPage = Math.max(1, config.currentPage - 1);
                } else if (targetPage === 'next') {
                    newPage = Math.min(totalPages, config.currentPage + 1);
                } else {
                    newPage = parseInt(targetPage);
                }

                if (newPage !== config.currentPage) {
                    renderPage(newPage);
                }
            });
        });
    }

    async function toggleStatus(itemId) {
        const endpoint = config.userRole === 'owner'
            ? `/Station/${itemId}/status`
            : `/Session/${itemId}/status`;

        const button = document.querySelector(`[data-id="${itemId}"] .update-btn`);
        const originalHtml = button.innerHTML;

        // Показать индикатор загрузки
        button.innerHTML = '<i class="fa fa-spinner fa-spin"></i> Updating';
        button.disabled = true;

        try {
            const response = await fetch(endpoint, {
                method: 'PATCH',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${localStorage.getItem('authToken')}`
                }
            });

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            

            const updatedStatus = await getUpdatedStatus(itemId);
            updateItemStatus(itemId, updatedStatus);
        } catch (error) {
            console.error('Failed to update status:', error);
            showToast('Error updating status', 'danger');
        } finally {
            // Восстановить кнопку
            button.innerHTML = originalHtml;
            button.disabled = false;
        }
    }

    async function getUpdatedStatus(itemId) {
        const endpoint = config.userRole === 'owner'
            ? `/Station/${itemId}`
            : `/Session/${itemId}`;

        const response = await fetch(endpoint, {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('authToken')}`
            }
        });

        if (!response.ok) throw new Error('Failed to get updated status');

        const itemData = await response.json();
        return itemData.status;
    }

    function updateItemStatus(itemId, newStatus) {
        // Обновляем данные в локальном хранилище
        const itemIndex = config.allItems.findIndex(item => item.id == itemId);

        if (itemIndex === -1) {
            console.error(`Item with ID ${itemId} not found in local data`);
            return;
        }

        config.allItems[itemIndex].status = newStatus;

        // Перерисовываем только измененный элемент
        const itemElement = document.querySelector(`[data-id="${itemId}"]`);
        if (itemElement) {
            const badge = itemElement.querySelector('.badge');
            if (badge) {
                badge.textContent = getStatusText(newStatus);
                badge.className =`badge ${ getStatusClass(newStatus) }`;
            } else {
                console.error('Badge element not found');
            }
        } else {
            console.error(`Item element with ID ${itemId} not found in DOM`);
        }
    }


    // Вспомогательные функции
    function getStatusClass(status) {
        if (config.userRole === 'owner') {
            return status === 'available' ? 'bg-success align-self-start' : 'bg-danger align-self-start';
        } else {
            return status === 'active' ? 'bg-success align-self-start' : 'bg-warning align-self-start';
        }
    }

    function getStatusText(status) {
        if (config.userRole === 'owner') {
            return status === 'available' ? 'аvailable' : 'unavailable';
        } else {
            return status === 'active' ? 'active' : 'complete';
        }
    }

    function showLoader() {
        elements.itemsContainer.innerHTML = `
            <div class="text-center my-5">
                <div class="spinner-border text-primary" role="status">
                    <span class="visually-hidden">Loading...</span>
                </div>
                <p>Loading data...</p>
            </div>
        `;
    }

    function showError() {
        elements.itemsContainer.innerHTML = `
            <div class="alert alert-danger text-center">
                Failed to load data. Please try again later.
                <button class="btn btn-sm btn-primary mt-2" onclick="loadData()">Retry</button>
            </div>
        `;
    }

    function showToast(message, type) {
        const toast = document.createElement('div');
        toast.className = `alert alert-${type} alert-dismissible fade show position-fixed`;
        toast.style.top = '20px';
        toast.style.right = '20px';
        toast.style.zIndex = '10000';
        toast.innerHTML = `
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        `;

        document.body.appendChild(toast);

        // Автоматическое скрытие
        setTimeout(() => {
            toast.remove();
        }, 3000);
    }

    // Инициализируем загрузку данных
    loadData();
});