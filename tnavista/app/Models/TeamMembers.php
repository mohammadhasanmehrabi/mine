<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class TeamMembers extends Model
{
    protected $fillable = [
        'full_name',
        'role',
        'bio',
        'photo_url',
        'skills',
        'github',
        'linkedin',
        'instagram',
        'portfolio',
        'security_level',
        'specialty'
    ];

    protected $casts = [
        'skills' => 'array',
    ];

    // متد برای ساخت خودکار آدرس گیت‌هاب
    public function setGithubAttribute($value)
    {
        if ($value) {
            $this->attributes['github'] = 'https://github.com/' . $value;
        }
    }

    // متد برای ساخت خودکار آدرس اینستاگرام
    public function setInstagramAttribute($value)
    {
        if ($value) {
            $this->attributes['instagram'] = 'https://instagram.com/' . $value;
        }
    }

    // متد برای ساخت خودکار آدرس لینکدین
    public function setLinkedinAttribute($value)
    {
        if ($value) {
            $this->attributes['linkedin'] = 'https://linkedin.com/in/' . $value;
        }
    }
}