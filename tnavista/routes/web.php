<?php

use App\Http\Controllers\Auth\RegisterController;
use App\Http\Controllers\Auth\LoginController;
use App\Http\Controllers\HomeController;
use App\Http\Controllers\Admin\AdminController;
use App\Http\Controllers\Admin\ArticleController;
use App\Http\Controllers\Admin\ProjectController;
use App\Http\Controllers\Admin\CommentController;
use App\Http\Controllers\Admin\TeamMemberController;
use App\Http\Controllers\Admin\UserManagementController;
use App\Http\Controllers\UserController;
use App\Http\Controllers\Admin\MessageController;
use App\Http\Controllers\TeamController;
use App\Http\Controllers\ArticelsController;
use App\Http\Controllers\ConttackController;
use Illuminate\Support\Facades\Route;
use App\Models\User;

Route::get('/register', [RegisterController::class, 'showRegistrationForm'])->name('register');
Route::post('/register', [RegisterController::class, 'register']);
Route::get('/login', [LoginController::class, 'showLoginForm'])->name('login');
Route::post('/login', [LoginController::class, 'login']);
Route::get('/', [HomeController::class, 'index'])->name('home');
Route::get('/projects', [HomeController::class, 'projects'])->name('projects');
Route::get('/team', [TeamController::class, 'index'])->name('team');
Route::get('/articles', [ArticelsController::class, 'index'])->name('articles');
Route::get('/articles/{id}', [ArticelsController::class, 'show'])->name('article.show');
Route::get('/contact', [ConttackController::class, 'index'])->name('contact');
Route::post('/contact', [ConttackController::class, 'store'])->name('contact.store');
Route::post('/contact/create', [ConttackController::class, 'store'])->name('contact.create');
Route::get('/careers', function () {
    return view('careers');
})->name('careers');


Route::middleware('auth')->group(function () {
    Route::get('/user/dashboard', [UserController::class, 'dashboard'])->name('user.dashboard');
    Route::get('/user/profile', [UserController::class, 'profile'])->name('user.profile');
    Route::post('/user/profile', [UserController::class, 'updateProfile'])->name('user.profile.update');

    // روت‌های پیام‌ها
    // روت‌های چت کاربران
    Route::get('/user/chats', [ConttackController::class, 'userChats'])->name('user.chats');
    Route::get('/user/chats/{id}', [ConttackController::class, 'showChat'])->name('user.chat.show');
    Route::post('/user/chats/{id}/message', [ConttackController::class, 'sendMessage'])->name('user.chat.send');
    Route::post('/user/chats/{id}/close', [ConttackController::class, 'closeChat'])->name('user.chat.close');
    Route::get('/user/chats/{id}/messages', [ConttackController::class, 'getNewMessages'])->name('user.chat.messages');

    // روت‌های تنظیمات
    Route::get('/user/settings', [UserController::class, 'settings'])->name('user.settings');
    Route::post('/user/settings', [UserController::class, 'updateSettings'])->name('user.settings.update');

    // روت‌های تاریخچه
    Route::get('/user/history', [UserController::class, 'history'])->name('user.history');

    Route::post('/logout', [UserController::class, 'logout'])->name('logout');
});

Route::middleware(['auth', 'is_admin'])->prefix('admin')->group(function () {
    Route::get('/dashboard', [AdminController::class, 'index'])->name('admin.dashboard');
    Route::get('/articles', [ArticleController::class, 'index'])->name('admin.articles');
    Route::get('/comments', [CommentController::class, 'index'])->name('admin.comments');

    // روت‌های مقالات
    Route::prefix('articles')->group(function () {
        Route::post('/', [ArticleController::class, 'store'])->name('admin.articles.store');
        Route::get('/{id}/edit', [ArticleController::class, 'edit'])->name('admin.articles.edit');
        Route::put('/{id}', [ArticleController::class, 'update'])->name('admin.articles.update');
        Route::delete('/{id}', [ArticleController::class, 'destroy'])->name('admin.articles.delete');
    });

    Route::group(['prefix' => 'projects'], function () {
        Route::get('/projects', [ProjectController::class, 'index'])->name('admin.projects');
        Route::post('/', [ProjectController::class, 'store'])->name('admin.projects.store');
        Route::put('/{id}', [ProjectController::class, 'update'],)->name('admin.projects.update');
        Route::delete('/{id}', [ProjectController::class, 'destroy'])->name('admin.projects.delete');
        Route::get('/{id}', [ProjectController::class, 'edit'])->name('admin.projects.edit');
    });

    Route::group(['prefix' => 'team-members'], function () {
        Route::get('/', [TeamMemberController::class, 'index'])->name('admin.team-members');
        Route::post('/', [TeamMemberController::class, 'store'])->name('admin.team-members.store');
        Route::put('/', [TeamMemberController::class, 'update'],)->name('admin.team-members.update');
        Route::delete('/{id}', [TeamMemberController::class, 'destroy'])->name('admin.team-members.delete');
    });

    Route::group(['prefix' => 'users'], function () {
        Route::get('/', [UserManagementController::class, 'index'])->name(name: 'admin.users');
        Route::delete('/user/{id}', [UserManagementController::class, 'destroy'])->name('admin.user.delete');
        Route::put('/user/{id}', [UserManagementController::class, 'update'])->name('admin.user.update');
    });

    // روت‌های پیام‌ها
    Route::prefix('messages')->group(function () {
        Route::get('/', [MessageController::class, 'index'])->name('admin.messages');
        Route::get('/{id}', [MessageController::class, 'show'])->name('admin.messages.show');
        Route::delete('/{id}', [MessageController::class, 'destroy'])->name('admin.messages.delete');
        Route::post('/', [MessageController::class, 'store'])->name('admin.messages.store');
        Route::post('/{id}/reply', [MessageController::class, 'reply'])->name('admin.messages.reply');
    });
});

// بررسی اینکه مدل‌ها درست لود شدن
ContactMessage::count();
ChatMessage::count();
