@extends('layouts.app')

@section('title', $article->title)

@push('scripts')
    <script src="{{ asset('js/article.js') }}"></script>
@endpush

@section('fullwidth')
<!-- Article Hero -->
<div class="article-detail-hero">
    <div class="container">
        <div class="article-detail-content">
            <div class="article-breadcrumb">
                <a href="{{ route('articles') }}">مقالات</a>
                <i class="fas fa-chevron-left"></i>
                <span>{{ $article->title }}</span>
            </div>
            
            <h1 class="article-detail-title">{{ $article->title }}</h1>
            
            <div class="article-detail-meta">
                @if($article->author_name)
                <div class="article-author-info">
                    <i class="fas fa-user-edit"></i>
                    <span>{{ $article->author_name }}</span>
                </div>
                @endif
                
                <div class="article-publish-date">
                    <i class="fas fa-calendar-alt"></i>
                    <span>{{ \Carbon\Carbon::parse($article->published_at)->format('Y/m/d H:i') }}</span>
                </div>
            </div>
        </div>
    </div>
</div>
@endsection

@section('content')
<div class="article-detail-container">
    <div class="article-detail-wrapper">
        <!-- Article Content -->
        <div class="article-detail-body">
            @if($article->summary)
            <div class="article-summary-box">
                <h3>خلاصه مقاله</h3>
                <p>{{ $article->summary }}</p>
            </div>
            @endif
            
            <div class="article-content-text">
                {!! nl2br(e($article->content)) !!}
            </div>
        </div>
        
        <!-- Article Sidebar -->
        <div class="article-sidebar">
            <div class="article-share">
                <h4>اشتراک‌گذاری</h4>
                <div class="share-buttons">
                    <a href="https://telegram.me/share/url?url={{ urlencode(request()->url()) }}&text={{ urlencode($article->title) }}" target="_blank" class="share-btn telegram">
                        <i class="fab fa-telegram"></i>
                        تلگرام
                    </a>
                    <a href="https://twitter.com/intent/tweet?url={{ urlencode(request()->url()) }}&text={{ urlencode($article->title) }}" target="_blank" class="share-btn twitter">
                        <i class="fab fa-twitter"></i>
                        توییتر
                    </a>
                    <a href="https://www.linkedin.com/sharing/share-offsite/?url={{ urlencode(request()->url()) }}" target="_blank" class="share-btn linkedin">
                        <i class="fab fa-linkedin"></i>
                        لینکدین
                    </a>
                </div>
            </div>
            
            <div class="back-to-articles">
                <a href="{{ route('articles') }}" class="back-btn">
                    <i class="fas fa-arrow-right"></i>
                    بازگشت به مقالات
                </a>
            </div>
        </div>
    </div>
</div>
@endsection 