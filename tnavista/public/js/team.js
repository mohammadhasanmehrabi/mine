document.addEventListener('DOMContentLoaded', () => {
    // افکت تایپ شعار با رنگ متغیر
    if (window.Typewriter) {
        new Typewriter('#typewriter', {
            loop: true,
            delay: 60,
            deleteSpeed: 30,
        })
        .typeString('<span style=\"color:#2563eb\">به تیم ما خوش آمدید!</span>')
        .pauseFor(1200)
        .deleteAll()
        .typeString('<span style=\"color:#60a5fa\">ما عاشق یادگیری و ساختنیم...</span>')
        .pauseFor(1200)
        .deleteAll()
        .typeString('<span style=\"color:#3b82f6\">با هم قوی‌تر و خلاق‌تر می‌شویم!</span>')
        .pauseFor(1800)
        .start();
    }

    const memberCards = document.querySelectorAll('.member-card');
    const modal = document.getElementById('profileModal');
    const modalBody = document.getElementById('modalBody');
    const closeModalBtn = document.getElementById('closeModal');

    // باز کردن مودال با کلیک روی کارت
    memberCards.forEach(card => {
        card.addEventListener('click', () => {
            const member = JSON.parse(card.getAttribute('data-member'));
            showMemberModal(member);
        });
    });

    // بستن مودال با دکمه ضربدر یا کلیک بیرون
    closeModalBtn.addEventListener('click', closeModal);
    modal.addEventListener('click', (e) => {
        if (e.target === modal) closeModal();
    });
    document.addEventListener('keydown', (e) => {
        if (e.key === 'Escape') closeModal();
    });

    function showMemberModal(member) {
        let skillsHtml = '';
        if (Array.isArray(member.skills)) {
            skillsHtml = member.skills.map(skill => `<span class='skill-tag'>${skill}</span>`).join(' ');
        }
        let linksHtml = '';
        if (member.github) {
            linksHtml += `<a href='${member.github}' target='_blank' class='profile-icon-link' title='GitHub'><i class="fab fa-github"></i></a>`;
        }
        if (member.instagram) {
            linksHtml += `<a href='${member.instagram}' target='_blank' class='profile-icon-link' title='Instagram'><i class="fab fa-instagram"></i></a>`;
        }
        if (member.linkedin) {
            linksHtml += `<a href='${member.linkedin}' target='_blank' class='profile-icon-link' title='LinkedIn'><i class="fab fa-linkedin"></i></a>`;
        }
        if (member.portfolio) {
            linksHtml += `<a href='${member.portfolio}' target='_blank' class='profile-icon-link' title='Portfolio'><i class="fas fa-globe"></i></a>`;
        }
        modalBody.innerHTML = `
            <div class="modal-flex">
                <div class="modal-avatar-col">
                    <img src='${member.photo_url ? member.photo_url : ''}' alt='${member.full_name}' class='modal-avatar-img'>
                </div>
                <div class="modal-info-col">
                    <h2 class='modal-member-name'><span id="typewriter-name"></span></h2>
                    <div class='modal-member-role'><span id="typewriter-role"></span></div>
                    <div class='modal-member-bio'><span id="typewriter-bio"></span></div>
                    <div class='skills-list modal-skills-list'>${skillsHtml}</div>
                    <div class='modal-links'>${linksHtml}</div>
                </div>
            </div>
        `;
        setTimeout(() => {
            if (window.Typewriter) {
                const nameEl = document.getElementById('typewriter-name');
                const roleEl = document.getElementById('typewriter-role');
                const bioEl = document.getElementById('typewriter-bio');
                if (nameEl) {
                    new Typewriter(nameEl, {
                        loop: false,
                        delay: 55,
                    })
                    .typeString(member.full_name)
                    .start();
                }
                if (roleEl) {
                    new Typewriter(roleEl, {
                        loop: false,
                        delay: 35,
                    })
                    .typeString(member.role ? member.role : '')
                    .start();
                }
                if (bioEl) {
                    new Typewriter(bioEl, {
                        loop: false,
                        delay: 18,
                    })
                    .typeString(member.bio ? member.bio : '')
                    .start();
                }
            } else {
                document.getElementById('typewriter-name').textContent = member.full_name;
                document.getElementById('typewriter-role').textContent = member.role ? member.role : '';
                document.getElementById('typewriter-bio').textContent = member.bio ? member.bio : '';
            }
        }, 100);
        modal.classList.add('active');
        document.body.style.overflow = 'hidden';
    }
    function closeModal() {
        modal.classList.remove('active');
        document.body.style.overflow = '';
    }
});
