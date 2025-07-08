@extends('admin.layout.master')

@section('styles')
<link rel="stylesheet" href="{{ asset('admin/css/messages.css') }}">
@endsection

@section('content')
<main class="main-content">
    <header class="content-header">
        <div class="header-left">
            <div class="header-title">
                <h1>مدیریت پیام‌ها</h1>
                <p class="header-subtitle">مشاهده و پاسخ به پیام‌های دریافتی</p>
            </div>
        </div>
        <div class="header-right">
            <div class="header-actions">
                <button class="btn btn-primary" onclick="openNewMessageModal()">
                    <i class="fas fa-plus"></i>
                    پیام جدید
                </button>
            </div>
        </div>
    </header>

    <div class="messages-content">
        <div class="messages-grid">
            @foreach($messages as $message)
            <div class="message-card {{ $message->is_read ? '' : 'unread' }}" data-id="{{ $message->id }}">
                <div class="message-header">
                    <div class="message-status">
                        <i class="fas {{ $message->is_read ? 'fa-envelope-open' : 'fa-envelope' }}"></i>
                        {{ $message->is_read ? 'خوانده شده' : 'خوانده نشده' }}
                    </div>
                    <div class="message-actions">
                        <button class="btn-icon" onclick="replyMessage({{ $message->id }})">
                            <i class="fas fa-reply"></i>
                        </button>
                        <button class="btn-icon delete" onclick="deleteMessage({{ $message->id }})">
                            <i class="fas fa-trash"></i>
                        </button>
                    </div>
                </div>
                <div class="message-content">
                    <div class="message-sender">
                        <i class="fas fa-user"></i>
                        <span>{{ $message->sender_name }}</span>
                    </div>
                    <h3 class="message-subject">{{ $message->subject ?: 'بدون موضوع' }}</h3>
                    <p class="message-preview">{{ Str::limit($message->message, 100) }}</p>
                    <div class="message-meta">
                        <div class="meta-item">
                            <i class="fas fa-envelope"></i>
                            <span>{{ $message->sender_contact }}</span>
                        </div>
                        <div class="meta-item">
                            <i class="fas fa-clock"></i>
                            <span>{{ $message->created_at->diffForHumans() }}</span>
                        </div>
                    </div>
                </div>
            </div>
            @endforeach
        </div>

        <div class="pagination">
            {{ $messages->links() }}
        </div>
    </div>
</main>

<!-- Modal مشاهده/پاسخ پیام -->
<div id="messageModal" class="modal">
    <div class="modal-content">
        <div class="modal-header">
            <h2>مشاهده پیام</h2>
            <button type="button" class="close-modal" onclick="closeModal()">&times;</button>
        </div>
        <div class="message-details">
            <div class="message-info">
                <div class="info-row">
                    <span class="info-label">فرستنده:</span>
                    <span class="info-value" id="modal-sender-name"></span>
                </div>
                <div class="info-row">
                    <span class="info-label">اطلاعات تماس:</span>
                    <span class="info-value" id="modal-sender-contact"></span>
                </div>
                <div class="info-row">
                    <span class="info-label">موضوع:</span>
                    <span class="info-value" id="modal-subject"></span>
                </div>
                <div class="info-row">
                    <span class="info-label">تاریخ ارسال:</span>
                    <span class="info-value" id="modal-date"></span>
                </div>
            </div>
            <div class="message-body">
                <div id="modal-message" class="message-text"></div>
            </div>
            <div class="message-reply">
                <h3>پاسخ به پیام</h3>
                <form id="replyForm">
                    <input type="hidden" id="messageId" name="messageId">
                    <div class="form-group">
                        <label for="reply">متن پاسخ</label>
                        <textarea id="reply" name="reply" rows="5" required></textarea>
                    </div>
                    <div class="form-actions">
                        <button type="submit" class="btn btn-primary">ارسال پاسخ</button>
                        <button type="button" class="btn btn-secondary" onclick="closeModal()">انصراف</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</div>
@endsection

@section('scripts')
<script src="{{ asset('admin/js/messages.js') }}"></script>
@endsection 