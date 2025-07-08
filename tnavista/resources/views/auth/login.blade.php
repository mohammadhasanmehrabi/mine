@extends('layouts.app')

@section('title', 'ورود')

@section('content')
<div class="login-container">
    <div class="login-card">
        <div class="login-header">
            <h1>خوش آمدید</h1>
            <p class="text-light">لطفاً وارد حساب کاربری خود شوید</p>
        </div>

        @if (session('success'))
            <div class="alert">{{ session('success') }}</div>
        @endif

        <form method="POST" action="{{ route('login') }}" class="login-form">
            @csrf
            <div class="form-group">
                <label for="email" class="form-label">ایمیل</label>
                <input type="email" 
                       class="form-control @error('email') is-invalid @enderror" 
                       id="email" 
                       name="email" 
                       value="{{ old('email') }}" 
                       placeholder="ایمیل خود را وارد کنید"
                       required>
                @error('email')
                    <div class="invalid-feedback">{{ $message }}</div>
                @enderror
            </div>

            <div class="form-group">
                <label for="password" class="form-label">رمز عبور</label>
                <input type="password" 
                       class="form-control @error('password') is-invalid @enderror" 
                       id="password" 
                       name="password" 
                       placeholder="رمز عبور خود را وارد کنید"
                       required>
                @error('password')
                    <div class="invalid-feedback">{{ $message }}</div>
                @enderror
            </div>

            <div class="remember-me">
                <input type="checkbox" class="form-check-input" name="remember" id="remember">
                <label for="remember">مرا به خاطر بسپار</label>
            </div>

            <button type="submit" class="btn-login">
                ورود به حساب کاربری
            </button>

            <div class="register-link">
                <p>حساب کاربری ندارید؟ <a href="{{ route('register') }}">ثبت‌نام کنید</a></p>
            </div>
        </form>
    </div>
</div>
@endsection