@extends('layouts.app')

@section('title', 'خانه')

@section('fullwidth')
<div class="tnav-home-slider">
  <div class="tnav-slider-wrapper">
    <div class="tnav-slide" style="background-image:url('/uploads/home/article.jpg')">
      <div class="tnav-slide-content">
        <h1>دنیای مقالات تخصصی</h1>
        <p>در بخش مقالات نویستا، جدیدترین دانش و تجربیات حوزه فناوری و مدیریت را بخوانید و همیشه به‌روز باشید.</p>
        <a href="/articles" class="tnav-slider-btn">مشاهده مقالات</a>
      </div>
    </div>
    <div class="tnav-slide" style="background-image:url('/uploads/home/home.jpg')">
      <div class="tnav-slide-content">
        <h1>به تیم نویستا بپیوندید</h1>
        <p>ما باور داریم با همکاری و همدلی، آینده‌ای روشن‌تر می‌سازیم. اگر به شعار ما \"با هم، قوی‌تر\" ایمان داری، همین حالا به تیم ما ملحق شو!</p>
        <a href="/contact" class="tnav-slider-btn">درخواست همکاری</a>
      </div>
    </div>
    <div class="tnav-slide" style="background-image:url('/uploads/home/project.jpg')">
      <div class="tnav-slide-content">
        <h1>پروژه‌های نوآورانه نویستا</h1>
        <p>با پروژه‌های متنوع و خلاقانه ما آشنا شوید و ببینید چگونه تکنولوژی را در خدمت رشد کسب‌وکارها قرار داده‌ایم.</p>
        <a href="/projects" class="tnav-slider-btn">مشاهده پروژه‌ها</a>
      </div>
    </div>
    <div class="tnav-slide" style="background-image:url('/uploads/home/team.jpg')">
      <div class="tnav-slide-content">
        <h1>تیم حرفه‌ای ما</h1>
        <p>نویستا متشکل از متخصصان جوان و باانگیزه است که با دانش و تجربه، مسیر موفقیت را هموار می‌کنند.</p>
        <a href="/team" class="tnav-slider-btn">آشنایی با تیم</a>
      </div>
    </div>
  </div>
  <div class="tnav-slider-dots"></div>
</div>
<!-- stats section -->
<div class="tnav-stats-section">
  <div class="tnav-stats-grid">
    <div class="tnav-stats-card">
      <div class="tnav-stats-icon"><i class="fas fa-project-diagram"></i></div>
      <div class="tnav-stats-number">{{ $projectsCount }}</div>
      <div class="tnav-stats-label">کل پروژه‌ها</div>
    </div>
    <div class="tnav-stats-card">
      <div class="tnav-stats-icon"><i class="fas fa-users"></i></div>
      <div class="tnav-stats-number">{{ $teamCount }}</div>
      <div class="tnav-stats-label">اعضای تیم</div>
    </div>
    <div class="tnav-stats-card">
      <div class="tnav-stats-icon"><i class="fas fa-user"></i></div>
      <div class="tnav-stats-number">{{ $usersCount }}</div>
      <div class="tnav-stats-label">کاربران سایت</div>
    </div>
    <div class="tnav-stats-card">
      <div class="tnav-stats-icon"><i class="fas fa-smile"></i></div>
      <div class="tnav-stats-number">98%</div>
      <div class="tnav-stats-label">رضایت مشتریان</div>
    </div>
  </div>
</div>
<!-- about section hero -->
<div class="tnav-about-hero">
  <div class="tnav-about-hero-badge">درباره نویستا</div>
  <h2 class="tnav-about-hero-title">جایی که رؤیاها کدنویسی می‌شوند.</h2>
  <div class="tnav-about-hero-desc">
    ما در نویستا، فقط کد نمی‌زنیم؛ ما به ایده‌ها جان می‌دهیم.<br>
    از طراحی سایت تا امنیت و برنامه‌های ویندوز،<br>
    با تیمی جوان، متعهد و خلاق
  </div>
</div>
<!-- about section modern grid (کارت‌ها راست، متن چپ) -->
<div class="tnav-about-modern">
  <div class="tnav-about-modern-row tnav-about-modern-reverse">
    <!-- ستون راست: گرید کارت‌ها -->
    <div class="tnav-about-modern-cards">
      <div class="tnav-about-modern-grid">
        <div class="tnav-about-modern-card tnav-fadein-up">
          <div class="tnav-about-modern-icon"><i class="fas fa-bullseye"></i></div>
          <div class="tnav-about-modern-card-title">ماموریت ما</div>
          <div class="tnav-about-modern-card-desc">تبدیل ایده‌ها به راهکارهای عملی و رشد پایدار کسب‌وکارها با تکیه بر نوآوری و کیفیت.</div>
        </div>
        <div class="tnav-about-modern-card tnav-fadein-up">
          <div class="tnav-about-modern-icon"><i class="fas fa-users-cog"></i></div>
          <div class="tnav-about-modern-card-title">تخصص ما</div>
          <div class="tnav-about-modern-card-desc">توسعه وب، اپلیکیشن، طراحی UI/UX و راهکارهای ابری برای پروژه‌های کوچک تا سازمانی.</div>
        </div>
        <div class="tnav-about-modern-card tnav-fadein-up">
          <div class="tnav-about-modern-icon"><i class="fas fa-lightbulb"></i></div>
          <div class="tnav-about-modern-card-title">ارزش‌های ما</div>
          <div class="tnav-about-modern-card-desc">خلاقیت، تعهد به کیفیت، یادگیری مستمر و رضایت مشتری، اصول بنیادین نویستا هستند.</div>
        </div>
        <div class="tnav-about-modern-card tnav-fadein-up">
          <div class="tnav-about-modern-icon"><i class="fas fa-handshake"></i></div>
          <div class="tnav-about-modern-card-title">همکاری و اعتماد</div>
          <div class="tnav-about-modern-card-desc">ما شریک قابل اعتماد شما در مسیر تحول دیجیتال و موفقیت پایدار هستیم.</div>
        </div>
      </div>
    </div>
    <!-- ستون چپ: متن معرفی و شعار -->
    <div class="tnav-about-modern-info tnav-fadein-right">
      <div class="tnav-about-modern-slogan"> نویستا؛ خلق رؤیا از دل کد.</div>
      <div class="tnav-about-modern-main">
        <p>در نویستا، ما صرفاً کدنویس نیستیم؛ ما معماران ایده‌ها هستیم.
        وقتی تو با یک فکر ناب سراغ ما می‌آیی، آن را مثل یک رؤیای زنده می‌بینیم که نیاز به بال دارد — و ما آن بال‌ها را می‌سازیم.</p>
        <p>ما تیمی جوان و پرانگیزه‌ایم که به طراحی وب‌سایت‌های حرفه‌ای، توسعه ربات‌های تلگرام، ساخت اپلیکیشن‌های ویندوز و اجرای پروژه‌های امنیتی می‌پردازیم. اما تخصص ما فقط در ابزار نیست؛ در درک «رویای تو» و رساندنش به واقعیت است.</p>
        <p>چه استارتاپ باشی، چه دانشجو، چه یک ذهن خلاق با یک جرقه‌ی اولیه؛<br>
        ما کنارت می‌ایستیم، با تو فکر می‌کنیم، خلق می‌کنیم و می‌سازیم.</p>
        <p>ویژگی ما فقط دانش فنی نیست؛ ما با اشتیاق کار می‌کنیم، با تعهد پیش می‌رویم، با خلاقیت می‌سازیم، و با سرعت تحویل می‌دهیم.<br>
        هدف ما؟ تبدیل شدن به یکی از قدرتمندترین و تاثیرگذارترین تیم‌های برنامه‌نویسی ایران و جهان — و این مسیر را از کنار تو شروع کرده‌ایم.</p>
        <p>در نویستا، تو فقط یک کارفرما نیستی — تو همراه ساختن آینده‌ای.<br><br>
        — تیم نویستا<br>
        جایی که کد به رویا معنا می‌دهد.</p>
      </div>
    </div>
  </div>
</div>
<!-- کارهای ما -->
<div class="tnav-works-section">
  <div class="tnav-works-header">
    <div class="tnav-works-badge">کارهای ما</div>
    <h2 class="tnav-works-title">خدمات حرفه‌ای ما</h2>
    <p class="tnav-works-desc">با تخصص و تجربه ما در زمینه‌های مختلف آشنا شوید</p>
  </div>
  <div class="tnav-works-grid">
    <!-- کارت ربات‌های تلگرام -->
    <div class="tnav-work-card tnav-work-card-telegram">
      <div class="tnav-work-card-inner">
        <div class="tnav-work-card-front">
          <div class="tnav-work-icon">
            <i class="fab fa-telegram-plane"></i>
          </div>
          <h3>ربات‌های تلگرام</h3>
          <p>طراحی و توسعه ربات‌های هوشمند تلگرام با قابلیت‌های پیشرفته</p>
        </div>
        <div class="tnav-work-card-back">
          <h3>ربات‌های تلگرام</h3>
          <ul>
            <li>ربات‌های مدیریت گروه</li>
            <li>ربات‌های خدماتی</li>
            <li>ربات‌های آموزشی</li>
            <li>ربات‌های تجاری</li>
          </ul>
          <a href="/services/telegram" class="tnav-work-btn">مشاهده خدمات</a>
        </div>
      </div>
    </div>
    <!-- کارت ساخت وبسایت -->
    <div class="tnav-work-card tnav-work-card-web">
      <div class="tnav-work-card-inner">
        <div class="tnav-work-card-front">
          <div class="tnav-work-icon">
            <i class="fas fa-globe"></i>
          </div>
          <h3>ساخت وبسایت</h3>
          <p>طراحی و توسعه وبسایت‌های حرفه‌ای و مدرن با آخرین تکنولوژی‌ها</p>
        </div>
        <div class="tnav-work-card-back">
          <h3>ساخت وبسایت</h3>
          <ul>
            <li>طراحی UI/UX حرفه‌ای</li>
            <li>توسعه فرانت‌اند</li>
            <li>توسعه بک‌اند</li>
            <li>بهینه‌سازی SEO</li>
          </ul>
          <a href="/services/web" class="tnav-work-btn">مشاهده خدمات</a>
        </div>
      </div>
    </div>
    <!-- کارت امنیت -->
    <div class="tnav-work-card tnav-work-card-security">
      <div class="tnav-work-card-inner">
        <div class="tnav-work-card-front">
          <div class="tnav-work-icon">
            <i class="fas fa-shield-alt"></i>
          </div>
          <h3>امنیت</h3>
          <p>ارائه راهکارهای امنیتی پیشرفته برای حفاظت از داده‌های شما</p>
        </div>
        <div class="tnav-work-card-back">
          <h3>امنیت</h3>
          <ul>
            <li>امنیت شبکه</li>
            <li>امنیت وبسایت</li>
            <li>امنیت اپلیکیشن</li>
            <li>مشاوره امنیتی</li>
          </ul>
          <a href="/services/security" class="tnav-work-btn">مشاهده خدمات</a>
        </div>
      </div>
    </div>
    <!-- کارت برنامه‌های ویندوز -->
    <div class="tnav-work-card tnav-work-card-windows">
      <div class="tnav-work-card-inner">
        <div class="tnav-work-card-front">
          <div class="tnav-work-icon">
            <i class="fab fa-windows"></i>
          </div>
          <h3>برنامه‌های ویندوز</h3>
          <p>توسعه نرم‌افزارهای کاربردی ویندوز با رابط کاربری مدرن</p>
        </div>
        <div class="tnav-work-card-back">
          <h3>برنامه‌های ویندوز</h3>
          <ul>
            <li>نرم‌افزارهای تجاری</li>
            <li>ابزارهای مدیریتی</li>
            <li>اپلیکیشن‌های کاربردی</li>
            <li>نرم‌افزارهای سفارشی</li>
          </ul>
          <a href="/services/windows" class="tnav-work-btn">مشاهده خدمات</a>
        </div>
      </div>
    </div>
  </div>
</div>
<!-- بخش انگیزشی و CTA مدرن پایین کارت‌ها -->
<div class="tnav-cta-section tnav-cta-modern" style="margin-bottom:-100px;">
  <div class="tnav-cta-bg"></div>
  <div class="tnav-cta-inner">
    <div class="tnav-cta-icon">
      <svg width="56" height="56" fill="none" stroke="white" stroke-width="2.7" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24"><path d="M2 16l10 6 10-6M2 8l10-6 10 6M2 8v8m20-8v8M6 6.7v4.6c0 1.2.8 2.3 2 2.7l4 1.3c.6.2 1.2.2 1.8 0l4-1.3c1.2-.4 2-1.5 2-2.7V6.7"/></svg>
    </div>
    <h2 class="tnav-cta-title">هیچ ایده‌ای احمقانه نیست<br>فقط نیازمنده فرصته!</h2>
    <div class="tnav-cta-desc">برای تحقق رویاهایت فقط یک قدم فاصله داری.<br>همین حالا با ما تماس بگیر یا نمونه پروژه‌ها را ببین!</div>
    <div class="tnav-cta-btns">
      <a href="/contact" class="tnav-cta-btn tnav-cta-btn-main">
        <svg width="22" height="22" fill="none" stroke="currentColor" stroke-width="2.2" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24"><path d="M2 16l10 6 10-6M2 8l10-6 10 6M2 8v8m20-8v8M6 6.7v4.6c0 1.2.8 2.3 2 2.7l4 1.3c.6.2 1.2.2 1.8 0l4-1.3c1.2-.4 2-1.5 2-2.7V6.7"/></svg>
        همکاری با ما
      </a>
      <a href="/projects" class="tnav-cta-btn tnav-cta-btn-outline">
        <svg width="22" height="22" fill="none" stroke="currentColor" stroke-width="2.2" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24"><circle cx="12" cy="12" r="10"/><path d="M8 12l2 2 4-4"/></svg>
        دیدن پروژه‌ها
      </a>
    </div>
    <div class="tnav-cta-features">
      <span><svg width="20" height="20" fill="none" stroke="#1EC9A6" stroke-width="2.2" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24"><circle cx="12" cy="12" r="10"/><path d="M8 12l2 2 4-4"/></svg> مشاوره رایگان و صمیمی</span>
      <span><svg width="20" height="20" fill="none" stroke="#1EC9A6" stroke-width="2.2" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24"><circle cx="12" cy="12" r="10"/><path d="M8 12l2 2 4-4"/></svg> تضمین کیفیت و پشتیبانی</span>
      <span><svg width="20" height="20" fill="none" stroke="#1EC9A6" stroke-width="2.2" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24"><circle cx="12" cy="12" r="10"/><path d="M8 12l2 2 4-4"/></svg> اجرای سریع و حرفه‌ای</span>
    </div>
  </div>
</div>
<!-- /end CTA -->
@endsection

@section('content')
@endsection




