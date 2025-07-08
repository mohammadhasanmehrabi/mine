@extends('layouts.app')

@section('title', 'ثبت‌نام')

@section('content')
<div class="login-container">
    <div class="login-card">
        <div class="login-header">
            <h1>ثبت‌نام</h1>
            <p class="text-light">لطفاً اطلاعات خود را وارد کنید</p>
        </div>
        @if (session('success'))
            <div class="alert">{{ session('success') }}</div>
        @endif
        <form method="POST" action="{{ route('register') }}" class="login-form">
            @csrf
            <div class="form-group">
                <label for="name" class="form-label">نام و نام‌خانوادگی</label>
                <input type="text" class="form-control @error('name') is-invalid @enderror" id="name" name="name" value="{{ old('name') }}" placeholder="نام و نام‌خانوادگی خود را وارد کنید" required>
                @error('name')
                    <div class="invalid-feedback">{{ $message }}</div>
                @enderror
            </div>
            <div class="form-group">
                <label for="email" class="form-label">ایمیل</label>
                <input type="email" class="form-control @error('email') is-invalid @enderror" id="email" name="email" value="{{ old('email') }}" placeholder="ایمیل خود را وارد کنید" required>
                @error('email')
                    <div class="invalid-feedback">{{ $message }}</div>
                @enderror
            </div>
            <div class="form-group">
                <label for="phone" class="form-label">شماره تلفن</label>
                <input type="text" class="form-control @error('phone') is-invalid @enderror" id="phone" name="phone" value="{{ old('phone') }}" placeholder="شماره تلفن خود را وارد کنید" required>
                @error('phone')
                    <div class="invalid-feedback">{{ $message }}</div>
                @enderror
            </div>
            <div class="form-group">
                <label for="password" class="form-label">رمز عبور</label>
                <input type="password" class="form-control @error('password') is-invalid @enderror" id="password" name="password" placeholder="رمز عبور خود را وارد کنید" required>
                @error('password')
                    <div class="invalid-feedback">{{ $message }}</div>
                @enderror
            </div>
            <div class="form-group">
                <label for="password_confirmation" class="form-label">تأیید رمز عبور</label>
                <input type="password" class="form-control" id="password_confirmation" name="password_confirmation" placeholder="تأیید رمز عبور" required>
            </div>
            <button type="submit" class="btn-login">ثبت‌نام</button>
            <div class="register-link">
                <p>حساب کاربری دارید؟ <a href="{{ route('login') }}">وارد شوید</a></p>
            </div>
        </form>
    </div>
</div>
@endsection