// Articles JavaScript
document.addEventListener('DOMContentLoaded', function() {
    
    // انیمیشن ورود کارت‌های مقالات
    const articleCards = document.querySelectorAll('.article-card');
    
    const observerOptions = {
        threshold: 0.1,
        rootMargin: '0px 0px -50px 0px'
    };
    
    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.style.opacity = '1';
                entry.target.style.transform = 'translateY(0)';
            }
        });
    }, observerOptions);
    
    articleCards.forEach((card, index) => {
        card.style.opacity = '0';
        card.style.transform = 'translateY(30px)';
        card.style.transition = `opacity 0.6s ease ${index * 0.1}s, transform 0.6s ease ${index * 0.1}s`;
        observer.observe(card);
    });
    
    // افکت hover برای کارت‌های مقالات
    articleCards.forEach(card => {
        card.addEventListener('mouseenter', function() {
            this.style.transform = 'translateY(-8px) scale(1.02)';
        });
        
        card.addEventListener('mouseleave', function() {
            this.style.transform = 'translateY(0) scale(1)';
        });
    });
    
    // انیمیشن برای دکمه‌های "ادامه مطلب"
    const readMoreBtns = document.querySelectorAll('.read-more-btn');
    readMoreBtns.forEach(btn => {
        btn.addEventListener('mouseenter', function() {
            const icon = this.querySelector('i');
            if (icon) {
                icon.style.transform = 'translateX(-5px)';
            }
        });
        
        btn.addEventListener('mouseleave', function() {
            const icon = this.querySelector('i');
            if (icon) {
                icon.style.transform = 'translateX(0)';
            }
        });
    });
    
    // انیمیشن برای دکمه‌های اشتراک‌گذاری
    const shareBtns = document.querySelectorAll('.share-btn');
    shareBtns.forEach(btn => {
        btn.addEventListener('mouseenter', function() {
            this.style.transform = 'translateY(-3px) scale(1.05)';
        });
        
        btn.addEventListener('mouseleave', function() {
            this.style.transform = 'translateY(0) scale(1)';
        });
    });
    
    // انیمیشن برای دکمه بازگشت
    const backBtn = document.querySelector('.back-btn');
    if (backBtn) {
        backBtn.addEventListener('mouseenter', function() {
            const icon = this.querySelector('i');
            if (icon) {
                icon.style.transform = 'translateX(5px)';
            }
        });
        
        backBtn.addEventListener('mouseleave', function() {
            const icon = this.querySelector('i');
            if (icon) {
                icon.style.transform = 'translateX(0)';
            }
        });
    }
    
    // انیمیشن ورود برای صفحه جزئیات مقاله
    const articleDetailHero = document.querySelector('.article-detail-hero');
    if (articleDetailHero) {
        articleDetailHero.style.opacity = '0';
        articleDetailHero.style.transform = 'translateY(-20px)';
        
        setTimeout(() => {
            articleDetailHero.style.transition = 'opacity 0.8s ease, transform 0.8s ease';
            articleDetailHero.style.opacity = '1';
            articleDetailHero.style.transform = 'translateY(0)';
        }, 100);
    }
    
    // انیمیشن ورود برای محتوای مقاله
    const articleDetailBody = document.querySelector('.article-detail-body');
    if (articleDetailBody) {
        articleDetailBody.style.opacity = '0';
        articleDetailBody.style.transform = 'translateY(30px)';
        
        setTimeout(() => {
            articleDetailBody.style.transition = 'opacity 0.8s ease 0.3s, transform 0.8s ease 0.3s';
            articleDetailBody.style.opacity = '1';
            articleDetailBody.style.transform = 'translateY(0)';
        }, 300);
    }
    
    // انیمیشن ورود برای sidebar
    const articleSidebar = document.querySelector('.article-sidebar');
    if (articleSidebar) {
        articleSidebar.style.opacity = '0';
        articleSidebar.style.transform = 'translateX(30px)';
        
        setTimeout(() => {
            articleSidebar.style.transition = 'opacity 0.8s ease 0.5s, transform 0.8s ease 0.5s';
            articleSidebar.style.opacity = '1';
            articleSidebar.style.transform = 'translateX(0)';
        }, 500);
    }
    
    // افکت تایپ برای عنوان مقاله در صفحه جزئیات
    const articleDetailTitle = document.querySelector('.article-detail-title');
    if (articleDetailTitle) {
        const text = articleDetailTitle.textContent;
        articleDetailTitle.textContent = '';
        articleDetailTitle.style.opacity = '1';
        
        let i = 0;
        const typeWriter = () => {
            if (i < text.length) {
                articleDetailTitle.textContent += text.charAt(i);
                i++;
                setTimeout(typeWriter, 50);
            }
        };
        
        setTimeout(typeWriter, 1000);
    }
    
    // انیمیشن برای breadcrumb
    const breadcrumb = document.querySelector('.article-breadcrumb');
    if (breadcrumb) {
        breadcrumb.style.opacity = '0';
        breadcrumb.style.transform = 'translateX(-20px)';
        
        setTimeout(() => {
            breadcrumb.style.transition = 'opacity 0.6s ease, transform 0.6s ease';
            breadcrumb.style.opacity = '1';
            breadcrumb.style.transform = 'translateX(0)';
        }, 200);
    }
    
    // انیمیشن برای meta اطلاعات
    const articleMeta = document.querySelector('.article-detail-meta');
    if (articleMeta) {
        const metaItems = articleMeta.querySelectorAll('div');
        metaItems.forEach((item, index) => {
            item.style.opacity = '0';
            item.style.transform = 'translateY(20px)';
            
            setTimeout(() => {
                item.style.transition = `opacity 0.6s ease ${0.8 + index * 0.2}s, transform 0.6s ease ${0.8 + index * 0.2}s`;
                item.style.opacity = '1';
                item.style.transform = 'translateY(0)';
            }, 800 + index * 200);
        });
    }
    
    // افکت parallax برای hero section
    window.addEventListener('scroll', function() {
        const scrolled = window.pageYOffset;
        const heroBanner = document.querySelector('.articles-hero-banner, .article-detail-hero');
        
        if (heroBanner) {
            const rate = scrolled * -0.5;
            heroBanner.style.transform = `translateY(${rate}px)`;
        }
    });
    
    // انیمیشن برای pagination
    const paginationLinks = document.querySelectorAll('.articles-pagination .page-link');
    paginationLinks.forEach((link, index) => {
        link.style.opacity = '0';
        link.style.transform = 'scale(0.8)';
        
        setTimeout(() => {
            link.style.transition = `opacity 0.4s ease ${index * 0.1}s, transform 0.4s ease ${index * 0.1}s`;
            link.style.opacity = '1';
            link.style.transform = 'scale(1)';
        }, 1000 + index * 100);
    });
    
    // افکت ripple برای دکمه‌ها
    function createRipple(event) {
        const button = event.currentTarget;
        const ripple = document.createElement('span');
        const rect = button.getBoundingClientRect();
        const size = Math.max(rect.width, rect.height);
        const x = event.clientX - rect.left - size / 2;
        const y = event.clientY - rect.top - size / 2;
        
        ripple.style.width = ripple.style.height = size + 'px';
        ripple.style.left = x + 'px';
        ripple.style.top = y + 'px';
        ripple.classList.add('ripple');
        
        button.appendChild(ripple);
        
        setTimeout(() => {
            ripple.remove();
        }, 600);
    }
    
    const buttons = document.querySelectorAll('.read-more-btn, .back-btn, .share-btn');
    buttons.forEach(button => {
        button.addEventListener('click', createRipple);
    });
    
    // اضافه کردن استایل ripple
    const style = document.createElement('style');
    style.textContent = `
        .ripple {
            position: absolute;
            border-radius: 50%;
            background: rgba(255, 255, 255, 0.3);
            transform: scale(0);
            animation: ripple-animation 0.6s linear;
            pointer-events: none;
        }
        
        @keyframes ripple-animation {
            to {
                transform: scale(4);
                opacity: 0;
            }
        }
        
        .read-more-btn, .back-btn, .share-btn {
            position: relative;
            overflow: hidden;
        }
    `;
    document.head.appendChild(style);
    
    // انیمیشن برای خلاصه مقاله
    const summaryBox = document.querySelector('.article-summary-box');
    if (summaryBox) {
        summaryBox.style.opacity = '0';
        summaryBox.style.transform = 'translateY(20px)';
        
        setTimeout(() => {
            summaryBox.style.transition = 'opacity 0.8s ease, transform 0.8s ease';
            summaryBox.style.opacity = '1';
            summaryBox.style.transform = 'translateY(0)';
        }, 1200);
    }
    
    // انیمیشن برای متن مقاله
    const contentText = document.querySelector('.article-content-text');
    if (contentText) {
        const paragraphs = contentText.querySelectorAll('p');
        paragraphs.forEach((p, index) => {
            p.style.opacity = '0';
            p.style.transform = 'translateY(20px)';
            
            setTimeout(() => {
                p.style.transition = `opacity 0.6s ease ${1.5 + index * 0.1}s, transform 0.6s ease ${1.5 + index * 0.1}s`;
                p.style.opacity = '1';
                p.style.transform = 'translateY(0)';
            }, 1500 + index * 100);
        });
    }
    
    // افکت loading برای تصاویر (اگر در آینده اضافه شوند)
    const images = document.querySelectorAll('img');
    images.forEach(img => {
        img.addEventListener('load', function() {
            this.style.opacity = '0';
            this.style.transform = 'scale(0.9)';
            
            setTimeout(() => {
                this.style.transition = 'opacity 0.6s ease, transform 0.6s ease';
                this.style.opacity = '1';
                this.style.transform = 'scale(1)';
            }, 100);
        });
    });
    
    // انیمیشن برای no-articles
    const noArticles = document.querySelector('.no-articles');
    if (noArticles) {
        noArticles.style.opacity = '0';
        noArticles.style.transform = 'translateY(30px)';
        
        setTimeout(() => {
            noArticles.style.transition = 'opacity 0.8s ease, transform 0.8s ease';
            noArticles.style.opacity = '1';
            noArticles.style.transform = 'translateY(0)';
        }, 300);
    }
    
    // افکت pulse برای آیکون no-articles
    const noArticlesIcon = document.querySelector('.no-articles-icon');
    if (noArticlesIcon) {
        noArticlesIcon.style.animation = 'pulse 2s infinite';
    }
    
    // اضافه کردن keyframe برای pulse
    const pulseStyle = document.createElement('style');
    pulseStyle.textContent = `
        @keyframes pulse {
            0% { transform: scale(1); }
            50% { transform: scale(1.1); }
            100% { transform: scale(1); }
        }
    `;
    document.head.appendChild(pulseStyle);
    
    console.log('Articles JavaScript loaded successfully!');
});
