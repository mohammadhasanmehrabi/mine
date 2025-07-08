// Article Management Scripts
document.addEventListener('DOMContentLoaded', function() {
    // Modal Management
    const modal = document.getElementById('articleModal');
    const closeModalBtn = document.querySelector('.close-modal');
    const articleForm = document.getElementById('articleForm');
    const searchInput = document.querySelector('.search-box input');

    // Open Modal
    window.openAddArticleModal = function() {
        modal.classList.add('show');
        articleForm.reset();
        document.getElementById('articleId').value = '';
        document.querySelector('.modal-header h2').textContent = 'افزودن مقاله جدید';
        
        // تنظیم تاریخ پیش‌فرض به زمان فعلی
        const now = new Date();
        now.setMinutes(now.getMinutes() - now.getTimezoneOffset());
        document.getElementById('published_at').value = now.toISOString().slice(0, 16);
    }

    // Close Modal
    function closeModal() {
        modal.classList.remove('show');
    }

    // Close Modal on Outside Click
    modal.addEventListener('click', (e) => {
        if (e.target === modal) {
            closeModal();
        }
    });

    // Close Modal Button
    closeModalBtn.addEventListener('click', closeModal);

    // Search Articles
    searchInput.addEventListener('input', () => {
        const searchTerm = searchInput.value.toLowerCase();
        const rows = document.querySelectorAll('.articles-table tbody tr');

        rows.forEach(row => {
            const title = row.cells[0].textContent.toLowerCase();
            const author = row.cells[1].textContent.toLowerCase();
            const summary = row.cells[2].textContent.toLowerCase();
            const isVisible = title.includes(searchTerm) || 
                            author.includes(searchTerm) || 
                            summary.includes(searchTerm);
            row.style.display = isVisible ? '' : 'none';
        });
    });

    // Form Submission
    articleForm.addEventListener('submit', async (e) => {
        e.preventDefault();
        const formData = new FormData(articleForm);
        const articleId = formData.get('articleId');
        
        try {
            let response;
            const data = {
                title: formData.get('title'),
                author_name: formData.get('author_name'),
                summary: formData.get('summary'),
                content: formData.get('content'),
                published_at: formData.get('published_at'),
                _token: document.querySelector('meta[name="csrf-token"]').content
            };

            if (articleId) {
                // ویرایش مقاله
                response = await fetch(`/dashboard/articles/${articleId}`, {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json',
                        'X-CSRF-TOKEN': document.querySelector('meta[name="csrf-token"]').content,
                        'Accept': 'application/json'
                    },
                    body: JSON.stringify(data)
                });
            } else {
                // افزودن مقاله جدید
                response = await fetch('/dashboard/articles', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'X-CSRF-TOKEN': document.querySelector('meta[name="csrf-token"]').content,
                        'Accept': 'application/json'
                    },
                    body: JSON.stringify(data)
                });
            }

            if (response.ok) {
                const result = await response.json();
                alert(result.message);
                window.location.reload();
            } else {
                const error = await response.json();
                throw new Error(error.message || 'خطا در ذخیره‌سازی');
            }
        } catch (error) {
            alert('خطا: ' + error.message);
        }
    });

    // Edit Article
    window.editArticle = async function(id) {
        try {
            const response = await fetch(`/dashboard/articles/${id}/edit`, {
                headers: {
                    'Accept': 'application/json'
                }
            });
            if (response.ok) {
                const article = await response.json();
                
                document.getElementById('articleId').value = article.id;
                document.getElementById('title').value = article.title;
                document.getElementById('author_name').value = article.author_name;
                document.getElementById('summary').value = article.summary;
                document.getElementById('content').value = article.content;
                document.getElementById('published_at').value = article.published_at.slice(0, 16);
                
                document.querySelector('.modal-header h2').textContent = 'ویرایش مقاله';
                modal.classList.add('show');
            } else {
                throw new Error('خطا در دریافت اطلاعات مقاله');
            }
        } catch (error) {
            alert('خطا: ' + error.message);
        }
    };

    // Delete Article
    window.deleteArticle = async function(id) {
        if (confirm('آیا از حذف این مقاله اطمینان دارید؟')) {
            try {
                const response = await fetch(`/dashboard/articles/${id}`, {
                    method: 'DELETE',
                    headers: {
                        'X-CSRF-TOKEN': document.querySelector('meta[name="csrf-token"]').content,
                        'Accept': 'application/json'
                    }
                });

                if (response.ok) {
                    const result = await response.json();
                    alert(result.message);
                    window.location.reload();
                } else {
                    const error = await response.json();
                    throw new Error(error.message || 'خطا در حذف مقاله');
                }
            } catch (error) {
                alert('خطا: ' + error.message);
            }
        }
    };
}); 