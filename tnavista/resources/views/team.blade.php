@extends('layouts.app')

@push('styles')
    <link href="{{ asset('css/team.css') }}" rel="stylesheet">
@endpush

@push('scripts')
    <script src="{{ asset('js/team.js') }}"></script>
@endpush

@section('title', 'تیم ما')

@section('fullwidth')
<div class="projects-hero-banner">
    <div class="projects-hero-bg"></div>
    <div class="projects-hero-content">
        <p class="projects-hero-title">قدرت خلاقیت در پروژه‌های ما</p>
        <p class="projects-hero-desc">هر پروژه یک داستان موفقیت است؛ با تیم ما آینده را بساز!</p>
    </div>
</div>

</div>
<div class="members-grid">
    @foreach($teamMembers as $member)
        <div class="member-card" data-member='@json($member)'>
            <div class="card-header">
                <img src="{{ $member->photo_url }}" alt="{{ $member->full_name }}" class="member-avatar">
            </div>
        </div>
    @endforeach
</div>

<!-- Modal نمایش جزئیات عضو -->
<div class="profile-modal" id="profileModal">
    <div class="modal-content">
        <div class="modal-header">
            <div class="access-granted">ACCESS GRANTED</div>
            <button class="close-btn" id="closeModal">&times;</button>
        </div>
        <div class="modal-body" id="modalBody">
            <!-- اطلاعات عضو به صورت داینامیک با JS قرار می‌گیرد -->
        </div>
    </div>
</div>
@endsection





