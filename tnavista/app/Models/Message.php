<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Message extends Model
{
    protected $table = 'contact_messages';

    protected $fillable = [
        'sender_name',
        'sender_contact',
        'subject',
        'message',
        'user_id',
        'is_read'
    ];

    protected $casts = [
        'created_at' => 'datetime',
        'updated_at' => 'datetime',
        'is_read' => 'boolean'
    ];

    protected $attributes = [
        'is_read' => false
    ];

    public function user()
    {
        return $this->belongsTo(User::class);
    }
} 