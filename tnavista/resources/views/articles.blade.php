@extends('layouts.app')

@section('title', 'مقالات')

@push('scripts')
    <script src="{{ asset('js/article.js') }}"></script>
@endpush

@section('fullwidth')
<!-- Hero Section -->
<div class="articles-hero-banner">
    <div class="container">
        <div class="articles-hero-content">
            <h1 class="articles-hero-title">مقالات و مطالب آموزشی</h1>
            <p class="articles-hero-desc">آخرین مقالات و مطالب آموزشی در زمینه تکنولوژی و برنامه‌نویسی</p>
        </div>
    </div>
</div>
@endsection

@section('content')
<!-- Articles Grid -->
<div class="articles-container">
    @if($articles->count() > 0)
        <div class="articles-grid">
            @foreach($articles as $article)
            <article class="article-card" data-aos="fade-up" data-aos-delay="{{ $loop->index * 100 }}">
                <div class="article-header">
                    <div class="article-meta">
                        <span class="article-date">
                            <i class="fas fa-calendar-alt"></i>
                            {{ \Carbon\Carbon::parse($article->published_at)->format('Y/m/d') }}
                        </span>
                        @if($article->author_name)
                        <span class="article-author">
                            <i class="fas fa-user-edit"></i>
                            {{ $article->author_name }}
                        </span>
                        @endif
                    </div>
                </div>
                
                <div class="article-content">
                    <h2 class="article-title">
                        <a href="{{ route('article.show', $article->id) }}">{{ $article->title }}</a>
                    </h2>
                    
                    @if($article->summary)
                    <p class="article-summary">{{ $article->summary }}</p>
                    @endif
                    
                    <div class="article-footer">
                        <a href="{{ route('article.show', $article->id) }}" class="read-more-btn">
                            <span>ادامه مطلب</span>
                            <i class="fas fa-arrow-left"></i>
                        </a>
                    </div>
                </div>
            </article>
            @endforeach
        </div>
        
        <!-- Pagination -->
        @if($articles->hasPages())
        <div class="articles-pagination">
            {{ $articles->links() }}
        </div>
        @endif
    @else
        <div class="no-articles">
            <div class="no-articles-icon">
                <i class="fas fa-newspaper"></i>
            </div>
            <h3>هیچ مقاله‌ای یافت نشد</h3>
            <p>در حال حاضر هیچ مقاله‌ای برای نمایش وجود ندارد.</p>
        </div>
    @endif
</div>
@endsection
