document.addEventListener('DOMContentLoaded', function() {
    // تنظیمات اولیه
    const sidebar = document.querySelector('.sidebar');
    const mainContent = document.querySelector('.main-content');
    const toggleBtn = document.querySelector('.toggle-sidebar');
    
    // اضافه کردن دکمه toggle به سایدبار
    if (!toggleBtn) {
        const toggleButton = document.createElement('button');
        toggleButton.className = 'toggle-sidebar';
        toggleButton.innerHTML = '<i class="fas fa-chevron-left"></i>';
        document.querySelector('.sidebar-header').appendChild(toggleButton);
    }

    // تابع toggle سایدبار
    function toggleSidebar() {
        sidebar.classList.toggle('collapsed');
        localStorage.setItem('sidebarCollapsed', sidebar.classList.contains('collapsed'));
    }

    // بررسی وضعیت ذخیره شده سایدبار
    if (localStorage.getItem('sidebarCollapsed') === 'true') {
        sidebar.classList.add('collapsed');
    }

    // اضافه کردن event listener به دکمه toggle
    document.querySelector('.toggle-sidebar').addEventListener('click', toggleSidebar);

    // فعال کردن لینک فعلی در منو
    const currentPath = window.location.pathname;
    const menuLinks = document.querySelectorAll('.sidebar-nav a');
    
    menuLinks.forEach(link => {
        if (link.getAttribute('href') === currentPath.split('/').pop()) {
            link.classList.add('active');
        }
    });

    // اضافه کردن انیمیشن به کارت‌ها
    const cards = document.querySelectorAll('.card');
    cards.forEach((card, index) => {
        card.style.animationDelay = `${index * 0.1}s`;
        
        card.addEventListener('mouseenter', function() {
            this.style.transform = 'translateY(-5px)';
        });
        
        card.addEventListener('mouseleave', function() {
            this.style.transform = 'translateY(0)';
        });
    });

    // اضافه کردن قابلیت responsive برای منو
    function handleResize() {
        if (window.innerWidth <= 768) {
            sidebar.classList.add('collapsed');
            mainContent.style.marginRight = '0';
        } else {
            if (localStorage.getItem('sidebarCollapsed') !== 'true') {
                sidebar.classList.remove('collapsed');
                mainContent.style.marginRight = '280px';
            }
        }
    }

    // اجرای تابع در لود صفحه و تغییر سایز
    window.addEventListener('resize', handleResize);
    handleResize();

    // اضافه کردن قابلیت نمایش/مخفی کردن سایدبار در موبایل
    const mobileMenuBtn = document.createElement('button');
    mobileMenuBtn.className = 'mobile-menu-btn';
    mobileMenuBtn.innerHTML = '<i class="fas fa-bars"></i>';
    document.querySelector('.content-header').prepend(mobileMenuBtn);

    mobileMenuBtn.addEventListener('click', () => {
        sidebar.classList.toggle('show');
    });

    // بستن سایدبار با کلیک خارج از آن در موبایل
    document.addEventListener('click', (e) => {
        if (window.innerWidth <= 768 && 
            !sidebar.contains(e.target) && 
            !mobileMenuBtn.contains(e.target) && 
            sidebar.classList.contains('show')) {
            sidebar.classList.remove('show');
        }
    });

    // اضافه کردن انیمیشن به آیکون‌های کارت‌ها
    const cardIcons = document.querySelectorAll('.card-icon i');
    cardIcons.forEach(icon => {
        icon.addEventListener('mouseenter', function() {
            this.style.transform = 'scale(1.2)';
            this.style.transition = 'transform 0.3s ease';
        });
        
        icon.addEventListener('mouseleave', function() {
            this.style.transform = 'scale(1)';
        });
    });
}); 