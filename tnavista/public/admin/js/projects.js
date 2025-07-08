// Project Management Scripts
document.addEventListener('DOMContentLoaded', function() {
    // Modal Management
    const modal = document.getElementById('projectModal');
    const closeModalBtn = document.querySelector('.close-modal');
    const projectForm = document.getElementById('projectForm');
    const searchInput = document.querySelector('.search-box input');
    const filterSelect = document.getElementById('statusFilter');

    // Open Modal - اضافه کردن به window object
    window.openAddProjectModal = function() {
        modal.style.display = 'block';
        projectForm.reset();
        document.getElementById('projectId').value = '';
        document.querySelector('.modal-header h2').textContent = 'افزودن پروژه جدید';
    };

    // Close Modal
    function closeModal() {
        modal.style.display = 'none';
    }

    // Close Modal on Outside Click
    modal.addEventListener('click', (e) => {
        if (e.target === modal) {
            closeModal();
        }
    });

    // Close Modal Button
    closeModalBtn.addEventListener('click', closeModal);

    // Search Projects
    searchInput.addEventListener('input', () => {
        const searchTerm = searchInput.value.toLowerCase();
        const projectCards = document.querySelectorAll('.project-card');

        projectCards.forEach(card => {
            const title = card.querySelector('h3').textContent.toLowerCase();
            const description = card.querySelector('.project-description').textContent.toLowerCase();
            const isVisible = title.includes(searchTerm) || description.includes(searchTerm);
            card.style.display = isVisible ? 'block' : 'none';
        });
    });

    // Filter Projects
    filterSelect.addEventListener('change', () => {
        const status = filterSelect.value;
        const projectCards = document.querySelectorAll('.project-card');

        projectCards.forEach(card => {
            if (status === 'all') {
                card.style.display = 'block';
            } else {
                const cardStatus = card.dataset.status;
                card.style.display = cardStatus === status ? 'block' : 'none';
            }
        });
    });

    // Form Submission
    projectForm.addEventListener('submit', async (e) => {
        e.preventDefault();
        
        const formData = new FormData(projectForm);
        formData.append('tags', JSON.stringify(tags));

        try {
            const response = await fetch(projectForm.action, {
                method: 'POST',
                body: formData,
                headers: {
                    'X-CSRF-TOKEN': document.querySelector('meta[name="csrf-token"]').content
                }
            });

            const result = await response.json();

            if (result.message) {
                closeModal();
                location.reload();
            } else {
                alert('خطا در ذخیره پروژه');
            }
        } catch (error) {
            console.error('Error saving project:', error);
            alert('خطا در ارتباط با سرور');
        }
    });

    // Edit Project
    window.editProject = async (id) => {
        try {
            const response = await fetch(`/dashboard/projects/${id}`);
            const project = await response.json();

            document.getElementById('projectId').value = project.id;
            document.getElementById('title').value = project.title;
            document.getElementById('description').value = project.description;
            document.getElementById('start_date').value = project.start_date;
            document.getElementById('end_date').value = project.end_date;
            document.getElementById('site_url').value = project.site_url;
            document.getElementById('status').value = project.status;
            document.getElementById('progress').value = project.progress;

            document.querySelector('.modal-header h2').textContent = 'ویرایش پروژه';
            modal.style.display = 'block';
        } catch (error) {
            console.error('Error fetching project:', error);
            alert('خطا در دریافت اطلاعات پروژه');
        }
    };

    // Delete Project
    window.deleteProject = async (id) => {
        if (confirm('آیا از حذف این پروژه اطمینان دارید؟')) {
            try {
                const response = await fetch(`/dashboard/projects/${id}`, {
                    method: 'DELETE',
                    headers: {
                        'X-CSRF-TOKEN': document.querySelector('meta[name="csrf-token"]').content
                    }
                });

                const result = await response.json();

                if (result.message) {
                    location.reload();
                } else {
                    alert('خطا در حذف پروژه');
                }
            } catch (error) {
                console.error('Error deleting project:', error);
                alert('خطا در ارتباط با سرور');
            }
        }
    };

    // Date Validation
    const startDateInput = document.getElementById('start_date');
    const endDateInput = document.getElementById('end_date');

    startDateInput.addEventListener('change', () => {
        endDateInput.min = startDateInput.value;
        if (endDateInput.value && endDateInput.value < startDateInput.value) {
            endDateInput.value = startDateInput.value;
        }
    });

    endDateInput.addEventListener('change', () => {
        if (endDateInput.value < startDateInput.value) {
            alert('تاریخ پایان نمی‌تواند قبل از تاریخ شروع باشد');
            endDateInput.value = startDateInput.value;
        }
    });

    // Progress Bar Update
    const progressInput = document.getElementById('progress');
    const progressValue = document.querySelector('.progress-value');

    progressInput.addEventListener('input', () => {
        progressValue.textContent = `${progressInput.value}%`;
    });

    // Tag Management
    const tagInput = document.getElementById('tagInput');
    const tagsList = document.getElementById('tagsList');
    let tags = [];

    tagInput.addEventListener('keydown', function(e) {
        if (e.key === 'Enter') {
            e.preventDefault();
            const tag = tagInput.value.trim();
            if (tag && !tags.includes(tag)) {
                tags.push(tag);
                updateTagsList();
                tagInput.value = '';
            }
        }
    });

    function updateTagsList() {
        tagsList.innerHTML = '';
        tags.forEach((tag, index) => {
            const tagElement = document.createElement('span');
            tagElement.className = 'tag';
            tagElement.innerHTML = `
                ${tag}
                <button type="button" onclick="removeTag(${index})">&times;</button>
            `;
            tagsList.appendChild(tagElement);
        });
    }

    window.removeTag = function(index) {
        tags.splice(index, 1);
        updateTagsList();
    };
}); 