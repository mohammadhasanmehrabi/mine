<!DOCTYPE html>
<html lang="fa" dir="rtl">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>T-Navista | @yield('title')</title>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/vazir-font@32.102.2/dist/font-face.css" rel="stylesheet">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@fontsource/iransans@4.0.0/index.min.css">
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@400;600&display=swap" rel="stylesheet">
    <!-- Custom CSS -->
    <link href="{{ asset('css/app.css') }}" rel="stylesheet">
    <link href="{{ asset('css/custom.css') }}" rel="stylesheet">
    @stack('styles')
</head>
<body>
    <nav class="navbar navbar-expand-lg navbar-dark" style="background: #F8FAFC;">
        <div class="container">
            <a class="navbar-brand d-flex align-items-center" href="{{ url('/') }}">
                <span class="navista-logo" style="display:inline-flex; align-items:center; justify-content:center; background:linear-gradient(135deg,#2A7CF7,#1EC9A6); border-radius:16px; padding:8px; margin-left:8px;">
                  <svg xmlns="http://www.w3.org/2000/svg" width="28" height="28" fill="none" stroke="white" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round" class="lucide lucide-zap" viewBox="0 0 24 24">
                    <polygon points="13 2 3 14 12 14 11 22 21 10 12 10 13 2"/>
                  </svg>
                </span>
                <span style="background: linear-gradient(90deg, #2A7CF7, #1EC9A6); -webkit-background-clip: text; -webkit-text-fill-color: transparent; font-weight: bold; font-size: 1.5rem;">Navista</span>
            </a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav ms-auto">
                    <li class="nav-item"><a class="nav-link" href="{{ url('/') }}">خانه</a></li>
                    <li class="nav-item"><a class="nav-link" href="{{ url('/team') }}">اعضای تیم</a></li>
                    <li class="nav-item"><a class="nav-link" href="{{ url('/projects') }}">پروژه‌ها</a></li>
                    <li class="nav-item"><a class="nav-link" href="{{ url('/articles') }}">مقالات</a></li>
                    <li class="nav-item"><a class="nav-link" href="welcome.blade.php">تماس با ما</a></li>
                    @auth
                        <li class="nav-item">
                            <a class="nav-link panel-icon" href="{{ url('/user/dashboard') }}" title="پنل کاربر">
                                <i class="fas fa-user-circle"></i>
                            </a>
                        </li>
                        @if (Auth::user()->is_admin)
                            <li class="nav-item">
                                <a class="nav-link panel-icon" href="{{ url('/dashboard') }}" title="پنل ادمین">
                                    <i class="fas fa-cog"></i>
                                </a>
                            </li>
                        @endif
                        <li class="nav-item">
                            <form method="POST" action="{{ route('logout') }}">
                                @csrf
                                <button type="submit" class="logout-button">
                                    <i class="fas fa-sign-out-alt"></i>
                                    خروج
                                </button>
                            </form>
                        </li>
                    @else
                        <li class="nav-item auth-buttons">
                            <a class="nav-link" href="{{ url('/login') }}" title="ورود">
                                <i class="fas fa-door-open"></i>
                            </a>
                        </li>
                    @endauth
                </ul>
            </div>
        </div>
    </nav>

    @yield('fullwidth')

    <div class="container mt-4">
        @yield('content')
    </div>

    <!-- Footer -->
    <footer class="footer-new bg-dark text-light pt-5 pb-3 mt-5">
        <div class="container">
            <div class="row gy-4 align-items-start">
                <!-- Logo & Description -->
                <div class="col-lg-3 col-md-6">
                    <div class="d-flex align-items-center mb-3">
                        <span class="navista-logo" style="display:inline-flex; align-items:center; justify-content:center; background:linear-gradient(135deg,#2A7CF7,#1EC9A6); border-radius:8px; padding:10px; margin-left:10px;">
                            <svg xmlns="http://www.w3.org/2000/svg" width="28" height="28" fill="none" stroke="white" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round" class="lucide lucide-zap" viewBox="0 0 24 24">
                                <polygon points="13 2 3 14 12 14 11 22 21 10 12 10 13 2"/>
                            </svg>
                        </span>
                        <span class="fw-bold fs-4 ms-2" style="background: linear-gradient(90deg, #2A7CF7, #1EC9A6); -webkit-background-clip: text; -webkit-text-fill-color: transparent;">Navista</span>
                    </div>
                    <p class="text-secondary mb-4" style="line-height:1.7;">Professional digital solutions that drive innovation and growth. We create exceptional experiences that connect brands with their audiences.</p>
                    <div class="d-flex gap-2">
                        <a href="" class="footer-social"><i class="fab fa-twitter"></i></a>
                        <a href="" class="footer-social"><i class="fab fa-linkedin-in"></i></a>
                        <a href="" class="footer-social"><i class="fab fa-github"></i></a>
                        <a href="" class="footer-social"><i class="fab fa-instagram"></i></a>
                    </div>
                </div>
                <!-- Quick Links -->
                <div class="col-lg-2 col-md-6">
                    <h5 class="fw-semibold mb-3">Quick Links</h5>
                    <ul class="list-unstyled text-secondary">
                        <li><a href="/" class="footer-link">Home</a></li>
                        <li><a href="/projects" class="footer-link">Projects</a></li>
                        <li><a href="/team" class="footer-link">Team</a></li>
                        <li><a href="/articles" class="footer-link">Articles</a></li>
                        <li><a href="/contact" class="footer-link">Contact</a></li>
                    </ul>
                </div>
                <!-- Services -->
                <div class="col-lg-3 col-md-6">
                    <h5 class="fw-semibold mb-3">Services</h5>
                    <ul class="list-unstyled text-secondary">
                        <li>Web Development</li>
                        <li>Mobile Apps</li>
                        <li>UI/UX Design</li>
                        <li>Digital Marketing</li>
                        <li>E-commerce</li>
                        <li>Consulting</li>
                    </ul>
                </div>
                <!-- Contact Info -->
                <div class="col-lg-4 col-md-6">
                    <h5 class="fw-semibold mb-3">Contact Info</h5>
                    <ul class="list-unstyled text-secondary mb-0">
                        <li class="mb-2"><i class="fas fa-envelope text-info me-2"></i> hello@navista.com</li>
                        <li class="mb-2"><i class="fas fa-phone text-info me-2"></i> +1 (555) 123-4567</li>
                        <li><i class="fas fa-map-marker-alt text-info me-2"></i> 123 Innovation Street<br>Tech City, TC 12345</li>
                    </ul>
                </div>
            </div>
            <hr class="my-4 border-secondary">
            <div class="d-flex flex-column flex-md-row justify-content-between align-items-center gap-3">
                <p class="mb-0 text-secondary small">© 2025 Navista. All rights reserved.</p>
                <div class="d-flex align-items-center gap-4">
                    <a href="/privacy" class="footer-link small">Privacy Policy</a>
                    <a href="/terms" class="footer-link small">Terms of Service</a>
                    <button onclick="window.scrollTo({top:0,behavior:'smooth'})" class="navista-logo border-0" style="background:linear-gradient(135deg,#2A7CF7,#1EC9A6); padding:8px; margin-left:0; cursor:pointer;">
                        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="none" stroke="white" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round" class="lucide lucide-arrow-up" viewBox="0 0 24 24">
                            <line x1="12" y1="19" x2="12" y2="5" />
                            <polyline points="5 12 12 5 19 12" />
                        </svg>
                    </button>
                </div>
            </div>
        </div>
    </footer>

    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
    <!-- Custom JS -->
    <!-- <script src="{{ asset('js/app.js') }}"></script> -->
    <script src="{{ asset('js/custom.js') }}"></script>
    @stack('scripts')
</body>
</html>

@push('styles')
<style>
.footer-new {
    background: #181F2A !important;
    color: #fff;
}
.footer-link {
    color: #94a3b8;
    text-decoration: none;
    transition: color 0.2s;
}
.footer-link:hover {
    color: #fff;
    text-decoration: underline;
}
.footer-social {
    display: inline-flex;
    align-items: center;
    justify-content: center;
    width: 40px;
    height: 40px;
    background: #232B39;
    border-radius: 8px;
    color: #fff;
    font-size: 1.2rem;
    margin-left: 4px;
    transition: background 0.3s, color 0.3s, transform 0.2s;
}
.footer-social:hover {
    background: linear-gradient(90deg, #2A7CF7, #1EC9A6);
    color: #fff;
    transform: translateY(-2px) scale(1.08);
}
</style>
@endpush