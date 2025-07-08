// ==== TNAVISTA HOME SLIDER ====
(function(){
  const slider = document.querySelector('.tnav-home-slider');
  if(!slider) return;
  
  const wrapper = slider.querySelector('.tnav-slider-wrapper');
  const slides = Array.from(wrapper.querySelectorAll('.tnav-slide'));
  const dotsContainer = slider.querySelector('.tnav-slider-dots');
  let current = 0, timer = null, interval = 4000;

  // ساخت دات‌ها
  dotsContainer.innerHTML = '';
  slides.forEach((_, i) => {
    const dot = document.createElement('span');
    dot.className = 'tnav-slider-dot' + (i === 0 ? ' active' : '');
    dot.addEventListener('click', () => goTo(i));
    dotsContainer.appendChild(dot);
  });
  const dots = Array.from(dotsContainer.children);

  function goTo(idx) {
    current = (idx + slides.length) % slides.length;
    wrapper.style.transform = `translateX(-${current * 100}vw)`;
    dots.forEach((d, i) => d.classList.toggle('active', i === current));
    // اجرای مجدد انیمیشن متن کارت
    const content = slides[current].querySelector('.tnav-slide-content');
    if(content) {
      content.querySelectorAll('h1, p, a').forEach((el) => {
        el.style.animation = 'none';
        el.offsetHeight; // ری‌فلو برای ریست انیمیشن
        el.style.animation = '';
      });
    }
    resetTimer();
  }

  function next() {
    goTo(current + 1);
  }

  function prev() {
    goTo(current - 1);
  }

  function resetTimer() {
    if(timer) clearInterval(timer);
    timer = setInterval(next, interval);
  }

  resetTimer();

  // توقف تایمر هنگام هاور
  slider.addEventListener('mouseenter', () => clearInterval(timer));
  slider.addEventListener('mouseleave', resetTimer);

  // دکمه‌های قبلی و بعدی
  const btnPrev = slider.querySelector('.tnav-slider-prev');
  const btnNext = slider.querySelector('.tnav-slider-next');
  if(btnPrev) btnPrev.onclick = prev;
  if(btnNext) btnNext.onclick = next;

  // اضافه کردن قابلیت swipe برای موبایل
  let touchStartX = 0;
  let touchEndX = 0;
  
  slider.addEventListener('touchstart', e => {
    touchStartX = e.changedTouches[0].screenX;
  }, false);
  
  slider.addEventListener('touchend', e => {
    touchEndX = e.changedTouches[0].screenX;
    handleSwipe();
  }, false);
  
  function handleSwipe() {
    const swipeThreshold = 50;
    const diff = touchStartX - touchEndX;
    
    if (Math.abs(diff) > swipeThreshold) {
      if (diff > 0) {
        next();
      } else {
        prev();
      }
    }
  }
})();
// ==== END TNAVISTA HOME SLIDER ====

// ==== TNAVISTA ABOUT SECTION FADE-IN ====
(function(){
  function revealAboutRows() {
    var aboutRows = document.querySelectorAll('.tnav-about-row.tnav-fadein-up');
    var windowHeight = window.innerHeight;
    aboutRows.forEach(function(row, i) {
      var rect = row.getBoundingClientRect();
      if(rect.top < windowHeight - 80) {
        setTimeout(function(){
          row.classList.add('visible');
        }, i * 180);
      }
    });
  }
  window.addEventListener('scroll', revealAboutRows);
  window.addEventListener('load', revealAboutRows);
})();

// ==== TNAVISTA ABOUT COMPACT SECTION FADE-IN ====
(function(){
  function revealAboutCards() {
    var aboutCards = document.querySelectorAll('.tnav-about-card');
    var windowHeight = window.innerHeight;
    aboutCards.forEach(function(card, i) {
      var rect = card.getBoundingClientRect();
      if(rect.top < windowHeight - 80) {
        setTimeout(function(){
          card.classList.add('visible');
        }, i * 180);
      }
    });
  }
  window.addEventListener('scroll', revealAboutCards);
  window.addEventListener('load', revealAboutCards);
})();

// ==== TNAVISTA ABOUT MODERN SECTION FADE-IN ====
(function(){
  function revealAboutModern() {
    var cards = document.querySelectorAll('.tnav-about-modern-card.tnav-fadein-up');
    var windowHeight = window.innerHeight;
    cards.forEach(function(card, i) {
      var rect = card.getBoundingClientRect();
      if(rect.top < windowHeight - 80) {
        setTimeout(function(){
          card.classList.add('tnav-inview');
        }, i * 180);
      }
    });
    // fade-in-right برای متن معرفی
    var info = document.querySelector('.tnav-about-modern-info.tnav-fadein-right');
    if(info) {
      var rect = info.getBoundingClientRect();
      if(rect.top < windowHeight - 80) info.classList.add('tnav-inview');
    }
  }
  window.addEventListener('scroll', revealAboutModern);
  window.addEventListener('load', revealAboutModern);
})();

// انیمیشن ورود از راست به چپ برای متن معرفی درباره ما (با تاخیر تدریجی)
function tnavFadeInRightOnScroll() {
  var container = document.querySelector('.tnav-about-modern-info');
  if (!container) return;
  var items = container.querySelectorAll('p, .tnav-about-modern-slogan');
  var baseDelay = 0;
  items.forEach(function(el, idx) {
    var rect = el.getBoundingClientRect();
    if (rect.top < window.innerHeight - 80) {
      setTimeout(function() {
        el.classList.add('tnav-inview');
      }, baseDelay + idx * 120);
    }
  });
}
window.addEventListener('scroll', tnavFadeInRightOnScroll);
window.addEventListener('DOMContentLoaded', tnavFadeInRightOnScroll);

// ==== TNAVISTA WORKS SECTION ANIMATIONS ====
(function(){
  function revealWorkCards() {
    var cards = document.querySelectorAll('.tnav-work-card');
    var windowHeight = window.innerHeight;
    
    cards.forEach(function(card) {
      var rect = card.getBoundingClientRect();
      if(rect.top < windowHeight - 100) {
        card.classList.add('tnav-inview');
      }
    });
  }
  
  window.addEventListener('scroll', revealWorkCards);
  window.addEventListener('load', revealWorkCards);
  
  // اضافه کردن افکت پارالاکس به کارت‌ها
  var cards = document.querySelectorAll('.tnav-work-card');
  cards.forEach(function(card) {
    card.addEventListener('mousemove', function(e) {
      var rect = card.getBoundingClientRect();
      var x = e.clientX - rect.left;
      var y = e.clientY - rect.top;
      
      var centerX = rect.width / 2;
      var centerY = rect.height / 2;
      
      var rotateX = (y - centerY) / 20;
      var rotateY = (centerX - x) / 20;
      
      card.style.transform = `perspective(1000px) rotateX(${rotateX}deg) rotateY(${rotateY}deg)`;
    });
    
    card.addEventListener('mouseleave', function() {
      card.style.transform = 'perspective(1000px) rotateX(0) rotateY(0)';
    });
  });
})();
// ==== END TNAVISTA WORKS SECTION ANIMATIONS ====

// ==== TNAVISTA WORKS SUPER MODERN ANIMATIONS ====
(function(){
  // انیمیشن ورود کارت‌ها با اسکرول
  function revealWorkCards() {
    var cards = document.querySelectorAll('.tnav-work-card');
    var windowHeight = window.innerHeight;
    cards.forEach(function(card, i) {
      var rect = card.getBoundingClientRect();
      if(rect.top < windowHeight - 80) {
        setTimeout(function(){
          card.classList.add('tnav-inview');
        }, i * 180);
      }
    });
  }
  window.addEventListener('scroll', revealWorkCards);
  window.addEventListener('load', revealWorkCards);

  // افکت پارالاکس با حرکت موس روی هر کارت
  var cards = document.querySelectorAll('.tnav-work-card');
  cards.forEach(function(card) {
    card.addEventListener('mousemove', function(e) {
      var rect = card.getBoundingClientRect();
      var x = e.clientX - rect.left;
      var y = e.clientY - rect.top;
      var centerX = rect.width / 2;
      var centerY = rect.height / 2;
      var rotateX = (y - centerY) / 16;
      var rotateY = (centerX - x) / 16;
      card.style.transform = `perspective(900px) rotateX(${rotateX}deg) rotateY(${rotateY}deg) scale(1.04)`;
    });
    card.addEventListener('mouseleave', function() {
      card.style.transform = '';
    });
  });
})();
// ==== END TNAVISTA WORKS SUPER MODERN ANIMATIONS ====

// ==== TNAVISTA WORKS ULTIMATE ANIMATIONS ====
(function(){
  // انیمیشن ورود کارت‌ها با اسکرول (spring)
  function revealWorkCards() {
    var cards = document.querySelectorAll('.tnav-work-card');
    var windowHeight = window.innerHeight;
    cards.forEach(function(card, i) {
      var rect = card.getBoundingClientRect();
      if(rect.top < windowHeight - 80) {
        setTimeout(function(){
          card.classList.add('tnav-inview');
        }, i * 180);
      }
    });
  }
  window.addEventListener('scroll', revealWorkCards);
  window.addEventListener('load', revealWorkCards);

  // افکت پارالاکس و نور موس (spotlight) روی هر کارت
  var cards = document.querySelectorAll('.tnav-work-card');
  cards.forEach(function(card) {
    card.addEventListener('mousemove', function(e) {
      var rect = card.getBoundingClientRect();
      var x = e.clientX - rect.left;
      var y = e.clientY - rect.top;
      var centerX = rect.width / 2;
      var centerY = rect.height / 2;
      var rotateX = (y - centerY) / 14;
      var rotateY = (centerX - x) / 14;
      card.style.transform = `perspective(1100px) rotateX(${rotateX}deg) rotateY(${rotateY}deg) scale(1.06)`;
      // نور موس
      card.style.setProperty('--spot-x', x + 'px');
      card.style.setProperty('--spot-y', y + 'px');
    });
    card.addEventListener('mouseleave', function() {
      card.style.transform = '';
      card.style.setProperty('--spot-x', '50%');
      card.style.setProperty('--spot-y', '50%');
    });
  });
})();
// ==== END TNAVISTA WORKS ULTIMATE ANIMATIONS ====

// ==== TNAVISTA PROJECTS FILTER & ANIMATION ====
(function(){
  if(!document.querySelector('.tnav-projects-grid')) return;
  // فعال‌سازی AOS
  if(window.AOS) AOS.init({ duration: 800, once: true });
  // فیلتر پروژه‌ها
  const filterBtns = document.querySelectorAll('.tnav-filter-btn');
  const projects = document.querySelectorAll('.tnav-project-card');
  filterBtns.forEach(btn => {
      btn.addEventListener('click', function() {
          filterBtns.forEach(b => b.classList.remove('active'));
          this.classList.add('active');
          const filter = this.getAttribute('data-filter');
          projects.forEach(card => {
              if(filter === 'all' || card.getAttribute('data-type') === filter || card.getAttribute('data-tags').includes(filter)) {
                  card.style.display = '';
              } else {
                  card.style.display = 'none';
              }
          });
      });
  });
})();
// ==== END TNAVISTA PROJECTS FILTER & ANIMATION ====

// ==== PROJECTS ULTIMATE EFFECTS ====
(function(){
  // افکت overlay و زوم عکس با CSS انجام می‌شود
  // اگر انیمیشن خاصی برای عنوان پروژه می‌خواهی، اینجا اضافه کن
  // مثال: fade-in یا تایپ شونده
  // فعلاً همه افکت‌ها با CSS انجام می‌شود و نیازی به JS نیست
})();
// ==== END PROJECTS ULTIMATE EFFECTS ====

// ==== CONTACT US FORM SCRIPT ====
document.addEventListener('DOMContentLoaded', function() {
    const form = document.getElementById('contactForm');
    if (!form) return;
    const btn = form.querySelector('.contact-submit-btn');
    const loader = btn.querySelector('.btn-loader');
    const btnText = btn.querySelector('.btn-text');
    const alertBox = form.querySelector('.contact-form-alert');
    const fields = ['sender_name', 'sender_contact', 'subject', 'message'];

    // اعتبارسنجی ساده سمت کلاینت
    function validate() {
        let valid = true;
        fields.forEach(f => {
            const input = form.querySelector(`[name="${f}"]`);
            const feedback = input.nextElementSibling;
            input.classList.remove('is-invalid');
            feedback.style.display = 'none';
            if (input.hasAttribute('required') && !input.value.trim()) {
                input.classList.add('is-invalid');
                feedback.textContent = 'این فیلد الزامی است.';
                feedback.style.display = 'block';
                valid = false;
            } else if (f === 'sender_contact' && input.value && !/^([\w\-.]+@[\w\-.]+\.[a-zA-Z]{2,}|09\d{9}|\+98\d{10,})$/.test(input.value.trim())) {
                input.classList.add('is-invalid');
                feedback.textContent = 'ایمیل یا شماره تماس معتبر وارد کنید.';
                feedback.style.display = 'block';
                valid = false;
            }
        });
        return valid;
    }

    form.addEventListener('submit', function(e) {
        e.preventDefault();
        alertBox.style.display = 'none';
        if (!validate()) return;
        btn.disabled = true;
        loader.style.display = 'inline-block';
        btnText.style.display = 'none';

        // ارسال AJAX
        fetch('/admin/messages', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'X-CSRF-TOKEN': document.querySelector('meta[name="csrf-token"]').getAttribute('content')
            },
            body: JSON.stringify({
                sender_name: form.sender_name.value,
                sender_contact: form.sender_contact.value,
                subject: form.subject.value,
                message: form.message.value,
                user_id: window.Laravel?.user?.id || null
            })
        })
        .then(res => res.json())
        .then(data => {
            loader.style.display = 'none';
            btnText.style.display = 'inline';
            btn.disabled = false;
            if (data.message) {
                alertBox.textContent = data.message;
                alertBox.className = 'contact-form-alert success';
                alertBox.style.display = 'block';
                form.reset();
            } else {
                alertBox.textContent = 'ارسال پیام با خطا مواجه شد.';
                alertBox.className = 'contact-form-alert error';
                alertBox.style.display = 'block';
            }
        })
        .catch(err => {
            loader.style.display = 'none';
            btnText.style.display = 'inline';
            btn.disabled = false;
            alertBox.textContent = 'ارسال پیام با خطا مواجه شد.';
            alertBox.className = 'contact-form-alert error';
            alertBox.style.display = 'block';
        });
    });
});
// ==== END CONTACT US FORM SCRIPT ====
