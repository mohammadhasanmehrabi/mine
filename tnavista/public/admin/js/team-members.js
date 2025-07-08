// مدیریت مهارت‌ها
let skills = [];

// مدیریت مودال
function openAddMemberModal() {
    const modal = document.getElementById('memberModal');
    const form = document.getElementById('memberForm');
    const title = modal.querySelector('h2');

    title.textContent = 'افزودن عضو جدید';
    form.reset();
    skills = [];
    updateSkillsList();

    modal.classList.add('show');
    document.body.style.overflow = 'hidden';
}

function closeModal() {
    const modal = document.getElementById('memberModal');
    modal.classList.remove('show');
    document.body.style.overflow = '';
}

// مدیریت مهارت‌ها
document.getElementById('skillInput').addEventListener('keypress', function(e) {
    if (e.key === 'Enter') {
        e.preventDefault();
        const skill = this.value.trim();
        if (skill && !skills.includes(skill)) {
            skills.push(skill);
            this.value = '';
            updateSkillsList();
        }
    }
});

function removeSkill(index) {
    skills.splice(index, 1);
    updateSkillsList();
}

function editMember(id) {
    const modal = document.getElementById('memberModal');
    modal.style.display = 'block';

    fetch(`/dashboard/team-members/${id}/edit`)
        .then(response => response.json())
        .then(member => {
            document.getElementById('full_name').value = member.full_name;
            document.getElementById('role').value = member.role;
            document.getElementById('bio').value = member.bio;
            
            // تنظیم مقادیر شبکه‌های اجتماعی
            if (member.github) {
                document.getElementById('github').value = member.github.split('/').pop();
            }
            if (member.linkedin) {
                document.getElementById('linkedin').value = member.linkedin.split('/').pop();
            }
            if (member.instagram) {
                document.getElementById('instagram').value = member.instagram.split('/').pop();
            }

            // تنظیم مهارت‌ها
            if (member.skills) {
                skills = member.skills;
                updateSkillsList();
            }

            // مقداردهی security_level و specialty
            if (member.security_level) {
                document.getElementById('security_level').value = member.security_level;
            }
            if (member.specialty) {
                document.getElementById('specialty').value = member.specialty;
            }
        })
        .catch(error => console.error("خطا در دریافت اطلاعات عضو:", error));
}

function updateSkillsList() {
    const skillsList = document.getElementById('skillsList');
    const skillsInput = document.getElementById('skillsInput');
    skillsList.innerHTML = '';
    skills.forEach((skill, index) => {
        const skillTag = document.createElement('span');
        skillTag.className = 'skill-tag';
        skillTag.innerHTML = `${skill} <i class="fas fa-times" onclick="removeSkill(${index})"></i>`;
        skillsList.appendChild(skillTag);
    });
    skillsInput.value = JSON.stringify(skills);
}

// بستن مودال با کلیک خارج از آن
window.onclick = function(event) {
    const modal = document.getElementById('memberModal');
    if (event.target == modal) {
        closeModal();
    }
}

// پیش‌نمایش تصویر
document.getElementById('photo').addEventListener('change', function(e) {
    const file = e.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = function(e) {
            const preview = document.createElement('img');
            preview.src = e.target.result;
            preview.style.maxWidth = '200px';
            preview.style.marginTop = '10px';
            preview.style.borderRadius = '8px';
            preview.style.boxShadow = '0 2px 4px rgba(0,0,0,0.1)';

            const container = document.getElementById('photo').parentElement;
            const oldPreview = container.querySelector('img');
            if (oldPreview) {
                container.removeChild(oldPreview);
            }
            container.appendChild(preview);
        }
        reader.readAsDataURL(file);
    }
});

// جستجو در اعضای تیم
document.querySelector('.search-box input').addEventListener('input', function(e) {
    const searchTerm = e.target.value.toLowerCase();
    const cards = document.querySelectorAll('.member-card');

    cards.forEach(card => {
        const name = card.querySelector('h3').textContent.toLowerCase();
        const role = card.querySelector('.member-role').textContent.toLowerCase();
        const bio = card.querySelector('.member-bio').textContent.toLowerCase();
        const skills = Array.from(card.querySelectorAll('.skill-tag')).map(tag => tag.textContent.toLowerCase());

        const isVisible = name.includes(searchTerm) ||
                         role.includes(searchTerm) ||
                         bio.includes(searchTerm) ||
                         skills.some(skill => skill.includes(searchTerm));

        card.style.display = isVisible ? 'block' : 'none';
    });
});