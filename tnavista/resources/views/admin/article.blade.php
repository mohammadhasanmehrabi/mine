@extends('admin.layout.master')

@section('styles')
<link rel="stylesheet" href="{{ asset('admin/css/articles.css') }}">
@endsection

@section('content')
    <main class="main-content">
        <header class="content-header">
            <div class="header-left">
                <div class="header-title">
                    <h1>مدیریت مقالات</h1>
                    <p class="header-subtitle">مدیریت و ویرایش مقالات وب‌سایت</p>
                </div>
            </div>

            <!-- <div class="header-center">
                <div class="search-box">
                    <div class="search-input-wrapper">
                        <i class="fas fa-search"></i>
                        <input type="text" placeholder="جستجو در مقالات...">
                        <button class="search-filters">
                            <i class="fas fa-sliders-h"></i>
                        </button>
                    </div>
                    <div class="search-suggestions">
                        <div class="suggestion-item">
                            <i class="fas fa-history"></i>
                            <span>جستجوهای اخیر</span>
                        </div>
                        <div class="suggestion-item">
                            <i class="fas fa-fire"></i>
                            <span>مقالات پربازدید</span>
                        </div>
                    </div>
                </div>
            </div> -->
            <div class="header-right">
                <div class="header-actions">
                    <button class="btn btn-primary" onclick="openAddArticleModal()">
                        <i class="fas fa-plus"></i>
                        افزودن مقاله جدید
                    </button>
                </div>
            </div>
        </header>

        <div class="articles-content">
            <div class="articles-grid">
                @foreach($articles as $article)
                <div class="article-card" data-id="{{ $article->id }}">
                    <div class="article-header">
                        <div class="article-status published">
                            <i class="fas fa-check-circle"></i>
                            منتشر شده
                        </div>
                        <div class="article-actions">
                            <button class="btn-icon" onclick="editArticle({{ $article->id }})">
                                <i class="fas fa-edit"></i>
                            </button>
                            <button class="btn-icon delete" onclick="deleteArticle({{ $article->id }})">
                                <i class="fas fa-trash"></i>
                            </button>
                        </div>
                    </div>
                    <div class="article-content">
                        <h3 class="article-title">{{ $article->title }}</h3>
                        <p class="article-author">
                            <i class="fas fa-user-edit"></i>
                            {{ $article->author_name }}
                        </p>
                        <p class="article-summary">{{ $article->summary }}</p>
                        <div class="article-meta">
                            <div class="meta-item">
                                <i class="fas fa-calendar"></i>
                                <span>{{ verta($article->published_at)->format('Y/m/d') }}</span>
                            </div>
                        </div>
                    </div>
                </div>
                @endforeach
            </div>
        </div>
    </main>
    </div>

    <!-- Modal افزودن/ویرایش مقاله -->
    <div id="articleModal" class="modal">
        <div class="modal-content">
            <div class="modal-header">
                <h2>افزودن مقاله جدید</h2>
                <button class="close-modal" onclick="closeModal()">&times;</button>
            </div>
            <form id="articleForm" action="{{ route('admin.articles.store') }}" method="POST">
                @csrf
                <input type="hidden" id="articleId" name="articleId">

                <div class="form-group">
                    <label for="title">عنوان مقاله</label>
                    <input type="text" id="title" name="title" required>
                </div>

                <div class="form-group">
                    <label for="author_name">نام نویسنده</label>
                    <input type="text" id="author_name" name="author_name" required>
                </div>

                <div class="form-group">
                    <label for="summary">خلاصه مقاله</label>
                    <textarea id="summary" name="summary" rows="3" required></textarea>
                </div>

                <div class="form-group">
                    <label for="content">محتوا</label>
                    <textarea id="content" name="content" rows="10" required></textarea>
                </div>

                <div class="form-group">
                    <label for="published_at">تاریخ انتشار</label>
                    <input type="datetime-local" id="published_at" name="published_at" required>
                </div>

                <div class="form-actions">
                    <button type="submit" class="btn btn-primary">ذخیره</button>
                    <button type="button" class="btn btn-secondary" onclick="closeModal()">انصراف</button>
                </div>
            </form>
        </div>
    </div>

    <script src="{{ asset('admin/js/articles.js') }}"></script>
@endsection
