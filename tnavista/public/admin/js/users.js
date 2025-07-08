// مدیریت مودال
function openNewUserModal() {
    const modal = document.getElementById('userModal');
    modal.classList.add('show');
    document.querySelector('.modal-header h2').textContent = 'افزودن کاربر جدید';
    document.getElementById('userForm').reset();
}

function closeModal() {
    const modal = document.getElementById('userModal');
    modal.classList.remove('show');
}

// مدیریت کاربران
function editUser(id) {
    const modal = document.getElementById('userModal');
    modal.classList.add('show');
    document.querySelector('.modal-header h2').textContent = 'ویرایش کاربر';
    // در اینجا می‌توانید اطلاعات کاربر را از سرور دریافت کنید
}

function deleteUser(id) {
    if (confirm('آیا از حذف این کاربر اطمینان دارید؟')) {
        // در اینجا می‌توانید درخواست حذف را به سرور ارسال کنید
        console.log('کاربر با شناسه ' + id + ' حذف شد');
    }
}

// مدیریت فرم کاربر
document.getElementById('userForm').addEventListener('submit', function(e) {
    e.preventDefault();
    const formData = new FormData(this);
    const userData = {
        fullName: formData.get('fullName'),
        email: formData.get('email'),
        phone: formData.get('phone'),
        password: formData.get('password'),
        isAdmin: formData.get('isAdmin') === 'on'
    };
    // در اینجا می‌توانید اطلاعات را به سرور ارسال کنید
    console.log('اطلاعات کاربر:', userData);
    closeModal();
});

// جستجو در کاربران
const searchInput = document.querySelector('.search-input-wrapper input');
searchInput.addEventListener('input', function(e) {
    const searchTerm = e.target.value.toLowerCase();
    const userCards = document.querySelectorAll('.user-card');
    
    userCards.forEach(card => {
        const name = card.querySelector('.user-name').textContent.toLowerCase();
        const email = card.querySelector('.info-item:nth-child(1) span').textContent.toLowerCase();
        const phone = card.querySelector('.info-item:nth-child(2) span').textContent.toLowerCase();
        
        if (name.includes(searchTerm) || email.includes(searchTerm) || phone.includes(searchTerm)) {
            card.style.display = 'block';
        } else {
            card.style.display = 'none';
        }
    });
});

// بستن مودال با کلیک خارج از آن
window.addEventListener('click', function(e) {
    const modal = document.getElementById('userModal');
    if (e.target === modal) {
        closeModal();
    }
}); 