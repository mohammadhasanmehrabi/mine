@extends('admin.layout.master')

@section('styles')
<link rel="stylesheet" href="{{ asset('admin/css/projects.css') }}">
@endsection

@section('content')
    <main class="main-content">
        <header class="content-header">
            <div class="header-title">
                <h1>مدیریت پروژه‌ها</h1>
                <p class="header-subtitle">مدیریت و پیگیری پروژه‌های در حال انجام و تکمیل شده</p>
            </div>
            <div class="header-actions">
                <div class="search-box">
                    <i class="fas fa-search"></i>
                    <input type="text" placeholder="">
                </div>
                <div class="filter-box">
                    <select id="statusFilter">
                        <option value="all">همه وضعیت‌ها</option>
                        <option value="in-progress">در حال انجام</option>
                        <option value="completed">تکمیل شده</option>
                        <option value="pending">در انتظار</option>
                    </select>
                </div>
                <button class="btn btn-primary" onclick="openAddProjectModal()">
                    <i class="fas fa-plus"></i>
                    افزودن پروژه جدید
                </button>
            </div>
        </header>

        <div class="projects-content">
            <div class="projects-grid">
                @foreach($projects as $project)
                <div class="project-card" data-status="{{ $project->status }}">
                    <div class="project-header">
                        <div class="project-status {{ $project->status }}">
                            @if($project->status == 'in-progress')
                                <i class="fas fa-spinner fa-spin"></i>
                            @elseif($project->status == 'completed')
                                <i class="fas fa-check-circle"></i>
                            @else
                                <i class="fas fa-clock"></i>
                            @endif
                            @switch($project->status)
                                @case('in-progress')
                                    در حال انجام
                                    @break
                                @case('completed')
                                    تکمیل شده
                                    @break
                                @case('pending')
                                    در انتظار
                                    @break
                            @endswitch
                        </div>
                        <div class="project-actions">
                            <button class="btn-icon" onclick="editProject({{ $project->id }})">
                                <i class="fas fa-edit"></i>
                            </button>
                            <button class="btn-icon" onclick="deleteProject({{ $project->id }})">
                                <i class="fas fa-trash"></i>
                            </button>
                        </div>
                    </div>
                    <div class="project-image">
                        <img src="{{ asset($project->image ?? 'images/default-project.jpg') }}" alt="{{ $project->title }}">
                    </div>
                    <div class="project-info">
                        <h3>{{ $project->title }}</h3>
                        <p class="project-description">{{ $project->description }}</p>
                        <div class="project-meta">
                            <div class="meta-item">
                                <i class="fas fa-calendar"></i>
                                <span>تاریخ شروع: {{ verta($project->start_date)->format('Y/m/d') }}</span>
                            </div>
                            @if($project->end_date)
                            <div class="meta-item">
                                <i class="fas fa-clock"></i>
                                <span>تاریخ پایان: {{ verta($project->end_date)->format('Y/m/d') }}</span>
                            </div>
                            @endif
                            @if($project->site_url)
                            <div class="meta-item">
                                <i class="fas fa-link"></i>
                                <a href="{{ $project->site_url }}" target="_blank" class="site-link">{{ $project->site_url }}</a>
                            </div>
                            @endif
                        </div>
                        <div class="project-progress">
                            <div class="progress-bar">
                                <div class="progress" style="width: {{ $project->progress }}%"></div>
                            </div>
                            <span class="progress-text">{{ $project->progress }}% تکمیل شده</span>
                        </div>
                        @if($project->tags)
                        <div class="project-tags">
                            @foreach(json_decode($project->tags) as $tag)
                                <span class="tag">{{ $tag }}</span>
                            @endforeach
                        </div>
                        @endif
                    </div>
                </div>
                @endforeach
            </div>
        </div>
    </main>

    <!-- Modal افزودن/ویرایش پروژه -->
    <div id="projectModal" class="modal">
        <div class="modal-content">
            <div class="modal-header">
                <h2>افزودن پروژه جدید</h2>
                <button class="close-modal" onclick="closeModal()">&times;</button>
            </div>
            <form id="projectForm" action="{{ route('admin.projects.store') }}" method="POST"
                enctype="multipart/form-data">
                @csrf
                <input type="hidden" id="projectId" name="projectId">

                <div class="form-group">
                    <label for="title">عنوان پروژه</label>
                    <input type="text" id="title" name="title" required>
                </div>

                <div class="form-group">
                    <label for="description">توضیحات</label>
                    <textarea id="description" name="description" rows="4" required></textarea>
                </div>

                <div class="form-row">
                    <div class="form-group">
                        <label for="start_date">تاریخ شروع</label>
                        <input type="date" id="start_date" name="start_date" required>
                    </div>
                    <div class="form-group">
                        <label for="end_date">تاریخ پایان</label>
                        <input type="date" id="end_date" name="end_date">
                    </div>
                </div>

                <div class="form-group">
                    <label for="site_url">آدرس سایت</label>
                    <input type="url" id="site_url" name="site_url" placeholder="https://example.com">
                </div>

                <div class="form-group">
                    <label for="status">وضعیت پروژه</label>
                    <select id="status" name="status" required>
                        <option value="pending">در انتظار</option>
                        <option value="in-progress">در حال انجام</option>
                        <option value="completed">تکمیل شده</option>
                    </select>
                </div>

                <div class="form-group">
                    <label for="model">مدل پروژه</label>
                    <select id="model" name="model" required>
                        <option value="web">web</option>
                        <option value="mobile">mobile</option>
                        <option value="uiux"> telegram</option>
                        <option value="cloud"> windows</option>
                    </select>
                </div>

                <div class="form-group">
                    <label for="progress">درصد پیشرفت</label>
                    <input type="range" id="progress" name="progress" min="0" max="100" value="0">
                    <span class="progress-value">0%</span>
                </div>

                <div class="form-group">
                    <label for="image">تصویر پروژه</label>
                    <input type="file" id="image" name="image" accept="image/*">
                </div>

                <div class="form-group">
                    <label for="tags">تگ‌ها</label>
                    <div class="tags-input">
                        <input type="text" id="tagInput" placeholder="تگ را وارد کنید و Enter را بزنید">
                        <div id="tagsList" class="tags-list"></div>
                    </div>
                </div>

                <div class="form-actions">
                    <button type="submit" class="btn btn-primary">ذخیره</button>
                    <button type="button" class="btn btn-secondary" onclick="closeModal()">انصراف</button>
                </div>
            </form>
        </div>
    </div>
    <script src="{{ asset('admin/js/projects.js') }}"></script>
@endsection

