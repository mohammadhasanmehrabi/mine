<?php

namespace App\Http\Controllers\Admin;

use App\Http\Controllers\Controller;
use App\Models\Message;
use Illuminate\Http\Request;

class MessageController extends Controller
{
    public function index()
    {
        $messages = Message::with('user')
            ->orderBy('sent_at', 'desc')
            ->paginate(10);
            
        return view('admin.messages', compact('messages'));
    }

    public function show($id)
    {
        $message = Message::with('user')->findOrFail($id);
        
        // علامت‌گذاری پیام به عنوان خوانده شده
        if (!$message->is_read) {
            $message->update(['is_read' => true]);
        }
        
        return response()->json($message);
    }

    public function destroy($id)
    {
        $message = Message::findOrFail($id);
        $message->delete();
        
        return response()->json([
            'message' => 'پیام با موفقیت حذف شد!'
        ]);
    }

    public function store(Request $request)
    {
        $validated = $request->validate([
            'sender_name' => 'required|string|max:100',
            'sender_contact' => 'required|string|max:100',
            'subject' => 'nullable|string|max:200',
            'message' => 'required|string',
            'user_id' => 'required|exists:users,id'
        ]);

        $message = Message::create($validated);

        return response()->json([
            'message' => 'پیام با موفقیت ارسال شد!',
            'data' => $message
        ]);
    }

    public function reply(Request $request, $id)
    {
        $validated = $request->validate([
            'reply' => 'required|string'
        ]);

        $message = Message::findOrFail($id);
        
        // اینجا می‌توانید منطق ارسال پاسخ را پیاده‌سازی کنید
        // مثلاً ارسال ایمیل یا ذخیره پاسخ در دیتابیس
        
        return response()->json([
            'message' => 'پاسخ با موفقیت ارسال شد!'
        ]);
    }
} 