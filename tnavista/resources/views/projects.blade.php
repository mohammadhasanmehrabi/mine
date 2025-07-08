@extends('layouts.app')

@section('title', 'پروژه‌ها')

@push('scripts')
    <script src="{{ asset('js/projects.js') }}"></script>
@endpush

@section('fullwidth')
<!-- بنر بالای کارت‌ها -->
<div class="projects-hero-banner">
    <div class="projects-hero-bg"></div>
    <div class="projects-hero-content">
        <p class="projects-hero-title">قدرت خلاقیت در پروژه‌های ما</p>
        <p class="projects-hero-desc">هر پروژه یک داستان موفقیت است؛ با تیم ما آینده را بساز!</p>
    </div>
</div>

<!-- فیلتر مدل پروژه‌ها -->
<div class="projects-model-filter-ultimate">
    <button class="model-filter-btn-ultimate active" data-model="all">
        <i class="fas fa-layer-group"></i>
        همه پروژه‌ها
    </button>
    <button class="model-filter-btn-ultimate" data-model="web">
        <i class="fas fa-code"></i>
        وب
    </button>
    <button class="model-filter-btn-ultimate" data-model="mobile">
        <i class="fas fa-mobile-alt"></i>
        موبایل
    </button>
    <button class="model-filter-btn-ultimate" data-model="telegram">
        <i class="fab fa-telegram-plane"></i>
        ربات تلگرام
    </button>
    <button class="model-filter-btn-ultimate" data-model="windows">
        <i class="fab fa-windows"></i>
        ویندوز
    </button>
</div>

<div class="projects-grid-ultimate" id="projects-grid">
    @foreach($projects as $project)
        <div class="project__card-ultimate" data-category="{{ $project->model ?? 'web' }}">
            <div class="project__imgbox">
                <img src="{{ asset($project->image ?? 'images/default-project.jpg') }}" alt="{{ $project->title ?? $project->name }}" class="project__img">
                <!-- وضعیت پروژه گوشه راست بالا -->
                <div class="project__status-badge project__status-badge--{{ $project->status }}">
                    @switch($project->status)
                        @case('in-progress')
                        @case('ongoing')
                        @case('در حال انجام')
                            <i class="fas fa-spinner fa-spin"></i> در حال انجام
                            @break
                        @case('completed')
                        @case('تکمیل شده')
                            <i class="fas fa-check-circle"></i> تکمیل شده
                            @break
                        @default
                            <i class="fas fa-clock"></i> در انتظار
                    @endswitch
                </div>
                <!-- Overlay با سه آیکون -->
                <div class="project__overlay">
                    <div class="project__links">
                        <a href="#" class="project__link project__link--details" title="about project">
                            <i class="fas fa-info-circle"></i>
                        </a>
                        @if($project->site_url)
                        <a href="{{ $project->site_url }}" class="project__link" title="live demo" target="_blank">
                            <i class="fas fa-external-link-alt"></i>
                        </a>
                        @endif
                        <a href="https://github.com/mohammadhasanmehrabi" class="project__link" title="github" target="_blank">
                            <i class="fab fa-github"></i>
                        </a>
                    </div>
                </div>
            </div>
            <div class="project__content-ultimate">
                <div class="project__meta-ultimate">
                    <div class="project__date-ultimate">
                        <i class="fas fa-calendar"></i>
                        @if($project->start_date && $project->end_date)
                            <span>{{ \Carbon\Carbon::parse($project->start_date)->format('Y/m/d') }} - {{ \Carbon\Carbon::parse($project->end_date)->format('Y/m/d') }}</span>
                        @elseif($project->start_date)
                            <span>{{ \Carbon\Carbon::parse($project->start_date)->format('Y/m/d') }}</span>
                        @endif
                    </div>
                </div>
                <h3 class="project__title-ultimate">
                    <span class="project__title-gradient">{{ $project->title ?? $project->name }}</span>
                </h3>
                <p class="project__description-ultimate">{{ Str::limit($project->description, 90) }}</p>
                <div class="project__tech-ultimate">
                    @php
                        $tags = [];
                        if (is_array($project->technologies)) {
                            $tags = $project->technologies;
                        } elseif (!empty($project->technologies) && is_string($project->technologies)) {
                            $tags = json_decode($project->technologies, true) ?? explode(',', $project->technologies);
                        } elseif (!empty($project->tags)) {
                            $tags = is_array($project->tags) ? $project->tags : (json_decode($project->tags, true) ?? explode(',', $project->tags));
                        }
                    @endphp
                    @foreach($tags as $tag)
                        <span class="tech-tag-ultimate">{{ trim($tag, "\"'[] ") }}</span>
                    @endforeach
                </div>
            </div>
        </div>
    @endforeach
</div>

<!-- بخش CTA پایین صفحه -->
<div class="tnav-cta-section tnav-cta-modern" style="margin-bottom:-100px;">
  <div class="tnav-cta-bg"></div>
  <div class="tnav-cta-inner">
    <div class="tnav-cta-icon">
      <svg width="56" height="56" fill="none" stroke="white" stroke-width="2.7" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24"><path d="M2 16l10 6 10-6M2 8l10-6 10 6M2 8v8m20-8v8M6 6.7v4.6c0 1.2.8 2.3 2 2.7l4 1.3c.6.2 1.2.2 1.8 0l4-1.3c1.2-.4 2-1.5 2-2.7V6.7"/></svg>
    </div>
        <h2 class="projects-cta-title">تو هم می‌تونی پروژه بعدی این لیست باشی!</h2>
        <div class="projects-cta-desc">برای تحقق رویاهایت فقط یک قدم فاصله داری.<br>همین حالا با ما تماس بگیر یا نمونه پروژه‌ها را ببین!</div>
        <div class="projects-cta-btns">
            <a href="/contact" class="projects-cta-btn projects-cta-btn-main">
                همکاری با ما
            </a>
            <a href="/projects" class="projects-cta-btn projects-cta-btn-outline">
                دیدن پروژه‌ها
            </a>
        </div>
    </div>
</div>
@endsection

