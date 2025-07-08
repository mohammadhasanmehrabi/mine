@extends('admin.layout.master')

@section('content')
    <main class="main-content">
        <div class="content-header">
            <div class="header-left">
                <div class="header-title">
                    <h1>مدیریت کاربران</h1>
                    <p class="header-subtitle">مدیریت و ویرایش اطلاعات کاربران</p>
                </div>
            </div>
        </div>

        <div class="users-content">
            <div class="users-grid">
                @foreach ($users as $user)
                    <div class="user-card admin">
                        <div class="user-header">
                            <div class="user-avatar">
                                <i class="fas fa-user-shield"></i>
                            </div>
                            <div class="user-actions">
                                <form action="{{ route('admin.user.update', $user->id) }}" method="POST" class="delete-form">
                                    @csrf
                                    @method('put')
                                    <button class="btn-icon delete" type="submit" onclick="deleteUser(1)">
                                        <i class='fa fa-handshake'></i>
                                    </button>
                                </form>
                                <form action="{{ route('admin.user.delete', $user->id) }}" method="POST" class="delete-form">
                                    @csrf
                                    @method('DELETE')
                                    <button class="btn-icon delete" type="submit" onclick="deleteUser(1)">
                                        <i class="fas fa-trash"></i>
                                    </button>
                                </form>
                            </div>
                        </div>
                        <div class="user-content">
                            <h3 class="user-name">{{ $user->name }}</h3>
                            <div class="user-info">
                                <div class="info-item">
                                    <i class="fas fa-envelope"></i>
                                    <span>{{ $user->email }}</span>
                                </div>
                            </div>
                            <div class="user-info">
                                <div class="info-item">
                                    <i class="fas fa-phone"></i>
                                    <span>{{ $user->phone }}</span>
                                </div>
                            </div>
                            <div class="user-status">
                                <span class="status-badge admin">{{ $user->is_admin == 1 ? 'مدیر سیستم ' : 'کاربر'}}</span>
                            </div>
                        </div>
                    </div>
                @endforeach


            </div>
        </div>
    </main>
    </div>
@endsection