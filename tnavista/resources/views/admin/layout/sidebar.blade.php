<aside class="sidebar">
    <div class="sidebar-header">
        <h2>پنل مدیریت</h2>
    </div>
    <nav class="sidebar-nav">
        <ul>
            <li>
            <a href="{{ route('admin.dashboard') }}" class="active">
                    <i class="fas fa-home"></i>
                    <span>داشبورد</span>
            </a>
            </li>
            <li>
                <a href="{{ route('admin.team-members') }}" <?php echo basename($_SERVER['PHP_SELF']) == 'team-members.php' ? 'class="active"' : ''; ?>>
                    <i class="fas fa-users"></i>
                    <span>مدیریت اعضای تیم</span>
                </a>
            </li>
            <li>
                <a href="{{ route('admin.projects') }}" <?php echo basename($_SERVER['PHP_SELF']) == 'projects.php' ? 'class="active"' : ''; ?>>
                    <i class="fas fa-project-diagram"></i>
                    <span>مدیریت پروژه‌ها</span>
                </a>
            </li>
            <li>
                <a href="{{ route('admin.articles') }}" <?php echo basename($_SERVER['PHP_SELF']) == 'articles.php' ? 'class="active"' : ''; ?>>
                    <i class="fas fa-newspaper"></i>
                    <span>مدیریت مقالات</span>
                </a>
            </li>
            <li>
                <a href="{{ route('admin.messages') }}" <?php echo basename($_SERVER['PHP_SELF']) == 'messages.php' ? 'class="active"' : ''; ?>>
                    <i class="fas fa-envelope"></i>
                    <span>مدیریت پیام‌ها</span>
                </a>
            </li>
            <li>
                <a href="{{ route('admin.users') }}" <?php echo basename($_SERVER['PHP_SELF']) == 'users.php' ? 'class="active"' : ''; ?>>
                    <i class="fas fa-user-cog"></i>
                    <span>مدیریت کاربران</span>
                </a>
            </li>
            <li class="nav-divider"></li>
            <li>
                <a href="{{ route('home') }}">
                    <i class="fas fa-external-link-alt"></i>
                    <span>بازگشت به سایت</span>
                </a>
            </li>
            <li>
                <a href="../logout.php" class="logout-link">
                    <i class="fas fa-sign-out-alt"></i>
                    <span>خروج از حساب کاربری</span>
                </a>
            </li>
        </ul>
    </nav>
</aside>