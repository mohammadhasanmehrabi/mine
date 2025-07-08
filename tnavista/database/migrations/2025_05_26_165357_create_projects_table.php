<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     */
    public function up(): void
    { {
            Schema::create('projects', function (Blueprint $table) {
                $table->id();
                $table->string('name'); // نام پروژه
                $table->text('description')->nullable(); // توضیحات پروژه
                $table->enum('status', ['ongoing', 'completed', 'pending'])->default('pending'); // وضعیت پروژه
                $table->date('start_date'); // تاریخ شروع
                $table->date('end_date')->nullable(); // تاریخ پایان
                $table->tinyInteger('completion_percentage')->default(0); // درصد تکمیل پروژه
                $table->json('technologies')->nullable(); // لیست فناوری‌های پروژه
                $table->string('image')->nullable(); // مسیر تصویر پروژه
                $table->timestamps();
            });
        }
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('projects');
    }
};
