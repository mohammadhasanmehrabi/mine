<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;

class UserController extends Controller
{
    // public function __construct()
    // {
    //     $this->middleware('auth'); // فقط کاربران واردشده دسترسی دارن
    // }

    public function dashboard()
    {
        $user = Auth::user();
        return view('user.dashboard', compact('user'));
    }

    public function profile()
    {
        $user = Auth::user();
        return view('user.profile', compact('user'));
    }

    public function updateProfile(Request $request)
    {
        $user = Auth::user();
        $request->validate([
            'name' => 'required|string|max:255',
            'email' => 'required|string|email|max:255|unique:users,email,' . $user->id,
        ]);

        $user->update([
            'name' => $request->name,
            'email' => $request->email,
        ]);

        return redirect()->route('user.profile')->with('success', 'Profile updated successfully!');
    }

    public function logout()
    {
        Auth::logout();
        return redirect()->route('login');
    }

    public function messages()
    {
        return view('user.messages');
    }

    public function newMessage()
    {
        return view('user.new-message');
    }

    public function sendMessage(Request $request)
    {
        // منطق ارسال پیام جدید
        return redirect()->route('user.messages')->with('success', 'پیام شما با موفقیت ارسال شد');
    }

    public function replyMessage($id)
    {
        return view('user.reply-message', ['message_id' => $id]);
    }

    public function sendReply(Request $request, $id)
    {
        // منطق ارسال پاسخ
        return redirect()->route('user.messages')->with('success', 'پاسخ شما با موفقیت ارسال شد');
    }

    public function settings()
    {
        return view('user.settings');
    }

    public function updateSettings(Request $request)
    {
        // منطق بروزرسانی تنظیمات
        return redirect()->route('user.settings')->with('success', 'تنظیمات با موفقیت بروزرسانی شد');
    }

    public function history()
    {
        return view('user.history');
    }
}
