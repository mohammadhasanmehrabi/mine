@extends('admin.layout.master')

@section('content')

<main class="main-content">
    <header class="content-header">
        <div class="header-title">
            <h1>مدیریت اعضای تیم</h1>
            <p class="header-subtitle">مدیریت و سازماندهی اعضای تیم توسعه</p>
        </div>
        <div class="header-actions">
            <button class="btn btn-primary" onclick="openAddMemberModal()">
                <i class="fas fa-plus"></i>
                افزودن عضو جدید
            </button>
        </div>
    </header>

    <div class="team-members-content">
        <div class="members-grid">
            @foreach($teamMembers as $member)
                <div class="member-card">
                    <div class="member-image">
                        <img src="{{ asset( $member->photo_url )}}" alt="{{ $member->full_name }}">
                        <div class="member-status online"></div>
                    </div>
                    <div class="member-info">
                        <h3>{{ $member->full_name }}</h3>
                        <span class="member-role">{{ $member->role }}</span>
                        <p class="member-bio">{{ $member->bio }}</p>
                        <div class="member-skills">
                            @foreach($member->skills as $skill)
                                <span class="skill-tag">{{ $skill }}</span>
                            @endforeach
                        </div>
                        <div class="member-social">
                            @if($member->github)
                                <a href="{{ $member->github }}" target="_blank" class="social-link">
                                    <i class="fab fa-github"></i>
                                </a>
                            @endif
                            @if($member->linkedin)
                                <a href="{{ $member->linkedin }}" target="_blank" class="social-link">
                                    <i class="fab fa-linkedin"></i>
                                </a>
                            @endif
                            @if($member->instagram)
                                <a href="{{ $member->instagram }}" target="_blank" class="social-link">
                                    <i class="fab fa-instagram"></i>
                                </a>
                            @endif
                            @if($member->portfolio)
                                <a href="{{ $member->portfolio }}" target="_blank" class="social-link">
                                    <i class="fas fa-briefcase"></i>
                                </a>
                            @endif
                        </div>
                        <div class="member-extra">
                            @if($member->security_level)
                                <span class="security-level">{{ $member->security_level }}</span>
                            @endif
                            @if($member->specialty)
                                <span class="specialty">{{ $member->specialty }}</span>
                            @endif
                        </div>
                    </div>
                    <div class="member-actions">
                        <button class="btn btn-edit" onclick="editMember({{ $member->id }})">
                            <i class="fas fa-edit"></i>
                            ویرایش
                        </button>
                        <form action="{{ route('admin.team-members.delete', $member->id) }}" method="POST">
                            @csrf
                            @method('DELETE')
                            <button class="btn btn-delete" onclick="deleteMember({{ $member->id }})">
                                <i class="fas fa-trash"></i>
                                حذف
                            </button>
                        </form>
                    </div>
                </div>
            @endforeach
        </div>
    </div>
</main>

<!-- Modal افزودن/ویرایش عضو -->
<div id="memberModal" class="modal">
<div class="modal-content">
    <div class="modal-header">
        <h2>افزودن عضو جدید</h2>
            <button class="btn btn-primary" onclick="openNewMemberModal()"></button>
        </div>

    <form id="memberForm" action="{{ route('admin.team-members.store') }}" method="POST" enctype="multipart/form-data">
        @csrf
        <div class="form-group">
            <label for="full_name">نام و نام خانوادگی</label>
            <input type="text" id="full_name" name="full_name" required>
        </div>

        <div class="form-group">
            <label for="role">سمت</label>
            <input type="text" id="role" name="role" required>
        </div>

        <div class="form-group">
            <label for="bio">درباره عضو</label>
            <textarea id="bio" name="bio" rows="4" required></textarea>
        </div>

        <div class="form-group">
            <label for="photo">تصویر عضو</label>
            <input type="file" id="photo" name="photo" accept="image/*">
        </div>

        <div class="form-group">
            <label for="skills">مهارت‌ها</label>
            <div class="skills-input">
                <input type="text" id="skillInput" placeholder="مهارت را وارد کنید و Enter را بزنید">
                <div id="skillsList" class="skills-list"></div>
                <input type="hidden" name="skills" id="skillsInput">
            </div>
        </div>

            <div class="form-group">
                <label for="github">نام کاربری گیت‌هاب</label>
                <div class="social-input">
                    <span class="social-prefix">github.com/</span>
                    <input type="text" id="github" name="github" placeholder="نام کاربری">
                </div>
            </div>

            <div class="form-group">
                <label for="linkedin">نام کاربری لینکدین</label>
                <div class="social-input">
                    <span class="social-prefix">linkedin.com/in/</span>
                    <input type="text" id="linkedin" name="linkedin" placeholder="نام کاربری">
                </div>
            </div>

            <div class="form-group">
                <label for="instagram">نام کاربری اینستاگرام</label>
                <div class="social-input">
                    <span class="social-prefix">instagram.com/</span>
                    <input type="text" id="instagram" name="instagram" placeholder="نام کاربری">
                </div>
            </div>

        <div class="form-group">
            <label for="portfolio">لینک پورتفولیو</label>
            <input type="text" id="portfolio" name="portfolio" placeholder="آدرس کامل یا نام کاربری">
        </div>
        <div class="form-actions">
            <button type="submit" class="btn btn-primary">ذخیره</button>
            <button type="button" class="btn btn-secondary" onclick="closeNewModal()">انصراف</button>
        </div>
    </form>
</div>
</div>

<script src="{{ asset('admin/js/team-members.js') }}"></script>
@endsection
