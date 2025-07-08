<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Articles extends Model
{
    protected $fillable = [
        'title',
        'summary',
        'content',
        'author_name',
        'published_at',
    ];
}