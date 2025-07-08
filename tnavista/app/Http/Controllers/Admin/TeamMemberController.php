<?php

namespace App\Http\Controllers\Admin;
use App\Models\TeamMembers;
use App\Http\Controllers\Controller;
use Illuminate\Http\Request;

class TeamMemberController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        $teamMembers = TeamMembers::all();
        return view('admin.teammembers', compact('teamMembers'));
    }

    /**
     * Show the form for creating a new resource.
     */
    public function create()
    {
        //
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(Request $request)
    {
        try {
            $request->validate([
                'full_name' => 'required|string|max:100',
                'role' => 'required|string|max:100',
                'bio' => 'required|string',
                'photo' => 'nullable|image|mimes:jpeg,png,jpg,gif|max:2048',
                'skills' => 'required',
                'github' => 'nullable|string|max:255',
                'linkedin' => 'nullable|string|max:255',
                'instagram' => 'nullable|string|max:255',
                'portfolio' => 'nullable|string|max:255',
                'security_level' => 'nullable|string|max:32',
                'specialty' => 'nullable|string|max:32',
            ]);

            $data = $request->all();

            // آپلود تصویر اگر وجود داشته باشد
            if ($request->hasFile('photo')) {
                $photo = $request->file('photo');
                $photoName = time() . '.' . $photo->getClientOriginalExtension();
                $photo->move(public_path('uploads/team'), $photoName);
                $data['photo_url'] = 'uploads/team/' . $photoName;
            }

            // تبدیل skills به آرایه اگر رشته JSON است
            if (is_string($data['skills'])) {
                $data['skills'] = json_decode($data['skills'], true);
            }
            
            TeamMembers::create($data);

            return redirect()->route('admin.team-members')
                ->with('success', 'عضو جدید با موفقیت اضافه شد.');
        } catch (\Exception $e) {
            return redirect()->back()
                ->with('error', 'خطا در ذخیره اطلاعات: ' . $e->getMessage())
                ->withInput();
        }
    }

    /**
     * Display the specified resource.
     */
    public function show(string $id)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(string $id)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(Request $request, string $id)
    {
        try {
            $request->validate([
                'full_name' => 'required|string|max:100',
                'role' => 'required|string|max:100',
                'bio' => 'required|string',
                'photo' => 'nullable|image|mimes:jpeg,png,jpg,gif|max:2048',
                'skills' => 'required',
                'github' => 'nullable|string|max:255',
                'linkedin' => 'nullable|string|max:255',
                'instagram' => 'nullable|string|max:255',
                'portfolio' => 'nullable|string|max:255',
                'security_level' => 'nullable|string|max:32',
                'specialty' => 'nullable|string|max:32',
            ]);

            $data = $request->all();

            // آپلود تصویر اگر وجود داشته باشد
            if ($request->hasFile('photo')) {
                $photo = $request->file('photo');
                $photoName = time() . '.' . $photo->getClientOriginalExtension();
                $photo->move(public_path('uploads/team'), $photoName);
                $data['photo_url'] = 'uploads/team/' . $photoName;
            }

            // تبدیل skills به آرایه اگر رشته JSON است
            if (is_string($data['skills'])) {
                $data['skills'] = json_decode($data['skills'], true);
            }

            $member = TeamMembers::findOrFail($id);
            $member->update($data);

            return redirect()->route('admin.team-members')
                ->with('success', 'عضو با موفقیت ویرایش شد.');
        } catch (\Exception $e) {
            return redirect()->back()
                ->with('error', 'خطا در ذخیره اطلاعات: ' . $e->getMessage())
                ->withInput();
        }
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(string $id)
    {
        $user = TeamMembers::findOrFail($id);
        $user->delete();

        return redirect()->back()->with('success', 'members deleted successfully.');
    }
}
