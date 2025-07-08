<main class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
    <div class="grid grid-cols-1 md:grid-cols-4 gap-6">
        <!-- سایدبار -->
        <aside class="w-64 bg-white shadow-sm h-[calc(100vh-4rem)] fixed right-0">
            <nav class="p-4 space-y-1">
                <a href="{{ route('user.dashboard') }}" class="flex items-center gap-3 p-3 rounded-lg hover:bg-gray-50 transition-colors {{ request()->routeIs('user.dashboard') ? 'bg-primary/5 text-primary' : 'text-gray-600' }}">
                    <i class="fas fa-home text-xl"></i>
                    <span>داشبورد</span>
                </a>

                <a href="{{ route('user.messages') }}" class="flex items-center gap-3 p-3 rounded-lg hover:bg-gray-50 transition-colors {{ request()->routeIs('user.messages') ? 'bg-primary/5 text-primary' : 'text-gray-600' }}">
                    <i class="fas fa-envelope text-xl"></i>
                    <span>پیام‌ها</span>
                    <span class="bg-red-500 text-white text-xs px-2 py-1 rounded-full">2</span>
                </a>

                <a href="{{ route('user.profile') }}" class="flex items-center gap-3 p-3 rounded-lg hover:bg-gray-50 transition-colors {{ request()->routeIs('user.profile') ? 'bg-primary/5 text-primary' : 'text-gray-600' }}">
                    <i class="fas fa-user text-xl"></i>
                    <span>پروفایل</span>
                </a>
            </nav>
        </aside>
