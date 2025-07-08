// مدیریت مودال
function openNewMessageModal() {
    const modal = document.getElementById('messageModal');
    modal.classList.add('show');
    document.querySelector('.modal-header h2').textContent = 'پیام جدید';
    document.getElementById('replyForm').reset();
}

function closeModal() {
    const modal = document.getElementById('messageModal');
    modal.classList.remove('show');
}

// مدیریت پیام‌ها
async function viewMessage(id) {
    try {
        const response = await fetch(`/dashboard/messages/${id}`, {
            headers: {
                'Accept': 'application/json'
            }
        });

        if (response.ok) {
            const message = await response.json();
            
            // Populate Modal
            document.getElementById('modal-sender-name').textContent = message.sender_name;
            document.getElementById('modal-sender-contact').textContent = message.sender_contact;
            document.getElementById('modal-subject').textContent = message.subject || 'بدون موضوع';
            document.getElementById('modal-date').textContent = new Date(message.created_at).toLocaleString('fa-IR');
            document.getElementById('modal-message').textContent = message.message;
            
            // Show Modal
            document.getElementById('messageModal').classList.add('show');
        } else {
            throw new Error('خطا در دریافت اطلاعات پیام');
        }
    } catch (error) {
        alert('خطا: ' + error.message);
    }
}

function replyMessage(id) {
    const modal = document.getElementById('messageModal');
    modal.classList.add('show');
    document.querySelector('.modal-header h2').textContent = 'پاسخ به پیام';
    // در اینجا می‌توانید اطلاعات پیام را از سرور دریافت کنید
}

// Delete Message
async function deleteMessage(id) {
    if (confirm('آیا از حذف این پیام اطمینان دارید؟')) {
        try {
            const response = await fetch(`/dashboard/messages/${id}`, {
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
                throw new Error(error.message || 'خطا در حذف پیام');
            }
        } catch (error) {
            alert('خطا: ' + error.message);
        }
    }
}

// مدیریت فرم پاسخ
document.getElementById('replyForm').addEventListener('submit', function(e) {
    e.preventDefault();
    const reply = document.getElementById('reply').value;
    // در اینجا می‌توانید پاسخ را به سرور ارسال کنید
    console.log('پاسخ ارسال شد:', reply);
    closeModal();
});

// جستجو در پیام‌ها
const searchInput = document.querySelector('.search-input-wrapper input');
searchInput.addEventListener('input', function(e) {
    const searchTerm = e.target.value.toLowerCase();
    const messageCards = document.querySelectorAll('.message-card');
    
    messageCards.forEach(card => {
        const subject = card.querySelector('.message-subject').textContent.toLowerCase();
        const sender = card.querySelector('.message-sender span').textContent.toLowerCase();
        const preview = card.querySelector('.message-preview').textContent.toLowerCase();
        
        if (subject.includes(searchTerm) || sender.includes(searchTerm) || preview.includes(searchTerm)) {
            card.style.display = 'block';
        } else {
            card.style.display = 'none';
        }
    });
});

// بستن مودال با کلیک خارج از آن
window.addEventListener('click', function(e) {
    const modal = document.getElementById('messageModal');
    if (e.target === modal) {
        closeModal();
    }
});

document.addEventListener('DOMContentLoaded', function() {
    // Modal Management
    const modal = document.getElementById('messageModal');
    const closeModalBtn = document.querySelector('.close-modal');

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
}); 