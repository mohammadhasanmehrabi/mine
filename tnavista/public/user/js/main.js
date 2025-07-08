// Mobile Menu Toggle
const mobileMenuBtn = document.querySelector('.mobile-menu-btn');
const navMenu = document.querySelector('.nav-menu');
const navLinks = document.querySelectorAll('.nav-link');

mobileMenuBtn.addEventListener('click', () => {
    mobileMenuBtn.classList.toggle('active');
    navMenu.classList.toggle('active');
});

// Close mobile menu when clicking on a link
navLinks.forEach(link => {
    link.addEventListener('click', () => {
        mobileMenuBtn.classList.remove('active');
        navMenu.classList.remove('active');
    });
});

// Smooth Scroll for Navigation Links
document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        e.preventDefault();
        const target = document.querySelector(this.getAttribute('href'));
        if (target) {
            target.scrollIntoView({
                behavior: 'smooth',
                block: 'start'
            });
            // Close mobile menu if open
            if (navMenu && navMenu.classList.contains('active')) {
                mobileMenuBtn.classList.remove('active');
                navMenu.classList.remove('active');
            }
        }
    });
});

// Header Scroll Effect
let lastScroll = 0;
const header = document.querySelector('.header');

window.addEventListener('scroll', () => {
    const currentScroll = window.pageYOffset;

    if (currentScroll <= 0) {
        header.classList.remove('scroll-up');
        return;
    }

    if (currentScroll > lastScroll && !header.classList.contains('scroll-down')) {
        // Scroll Down
        header.classList.remove('scroll-up');
        header.classList.add('scroll-down');
    } else if (currentScroll < lastScroll && header.classList.contains('scroll-down')) {
        // Scroll Up
        header.classList.remove('scroll-down');
        header.classList.add('scroll-up');
    }

    lastScroll = currentScroll;
});

// Intersection Observer for Fade-in Animation
const observerOptions = {
    root: null,
    rootMargin: '0px',
    threshold: 0.1
};

const observer = new IntersectionObserver((entries, observer) => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            entry.target.classList.add('fade-in');
            observer.unobserve(entry.target);
        }
    });
}, observerOptions);

// Observe all sections
document.querySelectorAll('section').forEach(section => {
    observer.observe(section);
});

// Project Slider
const projectSlider = document.querySelector('.projects-slider');
if (projectSlider) {
    let isDown = false;
    let startX;
    let scrollLeft;

    projectSlider.addEventListener('mousedown', (e) => {
        isDown = true;
        projectSlider.classList.add('active');
        startX = e.pageX - projectSlider.offsetLeft;
        scrollLeft = projectSlider.scrollLeft;
    });

    projectSlider.addEventListener('mouseleave', () => {
        isDown = false;
        projectSlider.classList.remove('active');
    });

    projectSlider.addEventListener('mouseup', () => {
        isDown = false;
        projectSlider.classList.remove('active');
    });

    projectSlider.addEventListener('mousemove', (e) => {
        if (!isDown) return;
        e.preventDefault();
        const x = e.pageX - projectSlider.offsetLeft;
        const walk = (x - startX) * 2;
        projectSlider.scrollLeft = scrollLeft - walk;
    });
}

// Form Validation
const contactForm = document.querySelector('.contact-form form');
if (contactForm) {
    contactForm.addEventListener('submit', (e) => {
        e.preventDefault();

        // Basic form validation
        const formData = new FormData(contactForm);
        let isValid = true;

        for (let [key, value] of formData.entries()) {
            if (!value.trim()) {
                isValid = false;
                const input = contactForm.querySelector(`[name="${key}"]`);
                input.classList.add('error');
            }
        }

        if (isValid) {
            // Here you would typically send the form data to your server
            console.log('Form submitted:', Object.fromEntries(formData));
            contactForm.reset();
            alert('پیام شما با موفقیت ارسال شد!');
        } else {
            alert('لطفاً تمام فیلدها را پر کنید.');
        }
    });

    // Remove error class on input
    contactForm.querySelectorAll('input, textarea').forEach(input => {
        input.addEventListener('input', () => {
            input.classList.remove('error');
        });
    });
}

// Service Cards Hover Effect
document.querySelectorAll('.service-card').forEach(card => {
    card.addEventListener('mouseenter', () => {
        card.style.transform = 'translateY(-10px) scale(1.02)';
    });

    card.addEventListener('mouseleave', () => {
        card.style.transform = 'translateY(0) scale(1)';
    });
});

// Team Social Links Hover Effect
document.querySelectorAll('.team-social a').forEach(link => {
    link.addEventListener('mouseenter', () => {
        link.style.color = 'var(--primary-color)';
    });

    link.addEventListener('mouseleave', () => {
        link.style.color = 'var(--light-text)';
    });
});

// Initialize Swiper
const swiper = new Swiper('.projects-slider', {
    slidesPerView: 1,
    spaceBetween: 30,
    loop: true,
    pagination: {
        el: '.swiper-pagination',
        clickable: true,
    },
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
    },
    breakpoints: {
        640: {
            slidesPerView: 2,
        },
        1024: {
            slidesPerView: 3,
        },
    },
});

// Initialize AOS
AOS.init({
    duration: 800,
    once: true
});

// Initialize Vanilla Tilt for 3D Cards
VanillaTilt.init(document.querySelectorAll(".card-3d"), {
    max: 25,
    speed: 400,
    glare: true,
    "max-glare": 0.5,
    scale: 1.05
});

// Auth Button Click Handler
const authBtn = document.getElementById('authBtn');
authBtn.addEventListener('click', () => {
    // Here you can add your authentication logic
    console.log('Auth button clicked');
});

// Parallax Effect for Hero Section
const hero = document.querySelector('.hero');
window.addEventListener('mousemove', (e) => {
    const { clientX, clientY } = e;
    const xPos = (clientX / window.innerWidth - 0.5) * 20;
    const yPos = (clientY / window.innerHeight - 0.5) * 20;

    hero.style.transform = `perspective(1000px) rotateY(${xPos}deg) rotateX(${-yPos}deg)`;
});

// Floating Cards Animation
const cards = document.querySelectorAll('.card-3d');
cards.forEach((card, index) => {
    card.style.animationDelay = `${index * 0.2}s`;
    card.classList.add('float');
});

// Add floating animation
const style = document.createElement('style');
style.textContent = `
    @keyframes float {
        0% {
            transform: translateY(0px) rotate(0deg);
        }
        50% {
            transform: translateY(-20px) rotate(5deg);
        }
        100% {
            transform: translateY(0px) rotate(0deg);
        }
    }

    .float {
        animation: float 6s ease-in-out infinite;
    }
`;
document.head.appendChild(style);

// Team Cards Hover Effect
document.querySelectorAll('.team-card').forEach(card => {
    card.addEventListener('mouseenter', () => {
        card.style.transform = 'translateY(-10px) scale(1.02)';
    });

    card.addEventListener('mouseleave', () => {
        card.style.transform = 'translateY(0) scale(1)';
    });
});

// Handle File Upload Preview
function handleFileUpload(input, previewElement) {
    if (input.files && input.files[0]) {
        const reader = new FileReader();
        reader.onload = function(e) {
            previewElement.src = e.target.result;
        }
        reader.readAsDataURL(input.files[0]);
    }
}

// Handle Form Validation
function validateForm(form) {
    const inputs = form.querySelectorAll('input[required], select[required], textarea[required]');
    let isValid = true;

    inputs.forEach(input => {
        if (!input.value.trim()) {
            isValid = false;
            input.classList.add('border-red-500');
        } else {
            input.classList.remove('border-red-500');
        }
    });

    return isValid;
}

// Handle Toast Messages
function showToast(message, type = 'success') {
    const toast = document.createElement('div');
    toast.className = `fixed bottom-4 left-4 px-4 py-2 rounded-lg text-white ${
        type === 'success' ? 'bg-green-500' :
        type === 'error' ? 'bg-red-500' :
        type === 'warning' ? 'bg-yellow-500' :
        'bg-blue-500'
    }`;
    toast.textContent = message;

    document.body.appendChild(toast);

    setTimeout(() => {
        toast.remove();
    }, 3000);
}

// Handle Message Sending
function sendMessage(input, container) {
    if (input.value.trim()) {
        const message = document.createElement('div');
        message.className = 'flex items-start gap-3 justify-end';
        message.innerHTML = `
            <div class="bg-primary text-white rounded-lg p-3 max-w-[70%]">
                <p>${input.value}</p>
                <span class="text-xs text-white/80 mt-1 block">${new Date().toLocaleTimeString()}</span>
            </div>
            <img src="/user/images/avatar.jpg" alt="User" class="w-8 h-8 rounded-full">
        `;
        container.appendChild(message);

        input.value = '';
        container.scrollTop = container.scrollHeight;
    }
}

// Handle File Upload
function handleFileUpload(input, previewContainer) {
    if (input.files && input.files[0]) {
        const file = input.files[0];
        const reader = new FileReader();

        reader.onload = function(e) {
            const preview = document.createElement('div');
            preview.className = 'relative inline-block p-2 bg-gray-100 rounded-lg';
            preview.innerHTML = `
                <img src="${e.target.result}" class="w-20 h-20 object-cover rounded">
                <button type="button" class="absolute -top-2 -right-2 w-6 h-6 bg-red-500 text-white rounded-full flex items-center justify-center">
                    <i class="fas fa-times"></i>
                </button>
            `;
            previewContainer.appendChild(preview);
        }

        reader.readAsDataURL(file);
    }
}

// Handle Form Submission
function handleFormSubmit(form, successMessage) {
    form.addEventListener('submit', function(e) {
        e.preventDefault();

        if (validateForm(form)) {
            // Show loading state
            const submitButton = form.querySelector('button[type="submit"]');
            const originalText = submitButton.innerHTML;
            submitButton.innerHTML = '<i class="fas fa-spinner fa-spin"></i> در حال ارسال...';
            submitButton.disabled = true;

            // Simulate API call
            setTimeout(() => {
                // Reset form
                form.reset();

                // Show success message
                showToast(successMessage);

                // Reset button
                submitButton.innerHTML = originalText;
                submitButton.disabled = false;
            }, 1500);
        }
    });
}
