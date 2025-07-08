<?php

namespace App\Models;

use Illuminate\Foundation\Auth\User as Authenticatable;

class User extends Authenticatable
{
    protected $fillable = [
        'name',
        'email',
        'phone',
        'password_hash',
        'is_admin',
    ];

    protected $hidden = [
        'password_hash',
        'remember_token',
    ];

    public function setPasswordHashAttribute($value)
    {
        $this->attributes['password_hash'] = \Illuminate\Support\Facades\Hash::make($value);
    }

    public function getAuthPassword()
    {
        return $this->password_hash;
    }

    public function contactMessages()
    {
        return $this->hasMany(ContactMessage::class, 'user_id');
    }

    public function adminContactMessages()
    {
        return $this->hasMany(ContactMessage::class, 'admin_id');
    }

    public function chatMessages()
    {
        return $this->hasMany(ChatMessage::class, 'sender_id');
    }
}