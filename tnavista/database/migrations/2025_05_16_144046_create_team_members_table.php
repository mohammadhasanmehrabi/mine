<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('team_members', function (Blueprint $table) {
            $table->id();
            $table->string('full_name', 100);
            $table->string('role', 100)->nullable();
            $table->text('bio')->nullable();
            $table->string('photo_url', 255)->nullable();
            $table->json('skills')->nullable();
            $table->string('github', 255)->nullable();
            $table->string('linkedin', 255)->nullable();
            $table->string('instagram', 255)->nullable();
            $table->string('portfolio', 255)->nullable();
            $table->string('security_level', 32)->nullable();
            $table->string('specialty', 32)->nullable();
            $table->timestamps();
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('team_members');
    }
};