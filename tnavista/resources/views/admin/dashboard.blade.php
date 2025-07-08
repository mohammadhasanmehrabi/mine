@extends('admin.layout.master')

@section('content')
<body>
    <div class="admin-container">
        <!-- Main Content -->
        <main class="main-content">
            <header class="content-header">
                <div class="header-title">
                    <h1>داشبورد</h1>
                </div>
                <div class="header-actions">
                    <div class="user-info">
                        <i class="fas fa-user-circle"></i>
                        <span>خوش آمدید، <?php echo $_SESSION['username'] ?? 'ادمین'; ?></span>
                    </div>
                </div>
            </header>

            <div class="dashboard-content">
                <div class="dashboard-cards">
                    <!-- کارت اعضای تیم -->
                    <div class="card">
                        <div class="card-header">
                            <div class="card-icon">
                                <i class="fas fa-users"></i>
                            </div>
                            <h3 class="card-title">اعضای تیم</h3>
                        </div>
                        <div class="card-body">
                            <div class="card-stats">
                                <div class="stat-item">
                                    <div class="stat-value">{{ $teamCount }}</div>
                                    <div class="stat-label">کل اعضا</div>
                                </div>
                                <div class="stat-item">
                                    <div class="stat-value">{{ $newTeamCount }}</div>
                                    <div class="stat-label">اعضای جدید</div>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                            <a href="{{ route('admin.team-members') }}" class="card-action">
                                <span>مشاهده و مدیریت</span>
                                <i class="fas fa-chevron-left"></i>
                            </a>
                        </div>
                    </div>

                    <!-- کارت پروژه‌ها -->
                    <div class="card">
                        <div class="card-header">
                            <div class="card-icon">
                                <i class="fas fa-project-diagram"></i>
                            </div>
                            <h3 class="card-title">پروژه‌ها</h3>
                        </div>
                        <div class="card-body">
                            <div class="card-stats">
                                <div class="stat-item">
                                    <div class="stat-value">{{ $projectsCount }}</div>                                    <div class="stat-label">کل پروژه‌ها</div>
                                </div>
                                <div class="stat-item">
                                    <div class="stat-label">در حال انجام</div>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                            <a href="{{ route('admin.projects') }}" class="card-action">
                                <span>مشاهده و مدیریت</span>
                                <i class="fas fa-chevron-left"></i>
                            </a>
                        </div>
                    </div>

                    <!-- کارت مقالات -->
                    <div class="card">
                        <div class="card-header">
                            <div class="card-icon">
                                <i class="fas fa-newspaper"></i>
                            </div>
                            <h3 class="card-title">مقالات</h3>
                        </div>
                        <div class="card-body">
                            <div class="card-stats">
                                <div class="stat-item">
                                    <div class="stat-value">{{$articlesCount }}</div>
                                    <div class="stat-label">کل مقالات</div>
                                </div>
                                <div class="stat-item">
                                <div class="stat-value">{{ $newArticlesCount }}</div>
                                <div class="stat-label">مقالات جدید</div>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                            <a href="{{  route('admin.articles') }}" class="card-action">
                                <span>مشاهده و مدیریت</span>
                                <i class="fas fa-chevron-left"></i>
                            </a>
                        </div>
                    </div>

                    <!-- کارت پیام‌ها -->
                    <div class="card">
                        <div class="card-header">
                            <div class="card-icon">
                                <i class="fas fa-envelope"></i>
                            </div>
                            <h3 class="card-title">پیام‌ها</h3>
                        </div>
                        <div class="card-body">
                            <div class="card-stats">
                                <div class="stat-item">
                                    <div class="stat-value">{{ $commentsCount }}</div>
                                    <div class="stat-label">کل پیام‌ها</div>
                                </div>
                                <div class="stat-item">
                                    <div class="stat-value">{{$newCommentsCount}}</div>
                                    <div class="stat-label">پیام‌های جدید</div>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                            <a href="{{ route('admin.comments')}}" class="card-action">
                                <span>مشاهده و مدیریت</span>
                                <i class="fas fa-chevron-left"></i>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </main>
    </div>

  
@endsection
