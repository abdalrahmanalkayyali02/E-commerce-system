// ─── Auth API ──────────────────────────────────────────────────────────────────
// Currently simulates network calls. Replace `simulate` with real fetch/axios.

import {
    LoginCredentials,
    RegisterFormState,
    OtpVerifyPayload,
    ResetPasswordPayload,
    AuthUser,
    AuthApiResponse,
} from '../types/auth.types';

const BASE_URL = process.env.REACT_APP_API_URL ?? 'http://localhost:5000/api';

/** Generic fetch wrapper (swap simulation with real calls when backend is ready) */
async function simulate<T>(ms: number, result: AuthApiResponse<T>): Promise<AuthApiResponse<T>> {
    await new Promise((res) => setTimeout(res, ms));
    return result;
}

// ─── Login ─────────────────────────────────────────────────────────────────────
export async function apiLogin(
    credentials: LoginCredentials
): Promise<AuthApiResponse<AuthUser>> {
    // TODO: replace with → fetch(`${BASE_URL}/auth/login`, { method:'POST', body: JSON.stringify(credentials) })
    return simulate(1600, {
        success: true,
        message: 'Login successful',
        data: {
            id: 'usr_001',
            email: credentials.email,
            firstName: 'John',
            lastName: 'Doe',
            role: 'customer' as const,
        },
    });
}

// ─── Register ──────────────────────────────────────────────────────────────────
export async function apiRegister(
    payload: RegisterFormState
): Promise<AuthApiResponse> {
    // TODO: replace with real multipart/form-data fetch:
    // const fd = new FormData();
    // Object.entries(payload).forEach(([k, v]) => v != null && fd.append(k, v instanceof File ? v : String(v)));
    // return fetch(`${BASE_URL}/auth/register`, { method: 'POST', body: fd }).then(r => r.json());
    void payload;
    return simulate(1800, {
        success: true,
        message: 'Registration successful — OTP sent to your email.',
    });
}

// ─── Send OTP ──────────────────────────────────────────────────────────────────
export async function apiSendOtp(
    email: string,
    purpose: 'register' | 'forgot-password'
): Promise<AuthApiResponse> {
    // TODO: replace with → fetch(`${BASE_URL}/auth/otp/send`, { method:'POST', body: JSON.stringify({ email, purpose }) })
    return simulate(1200, {
        success: true,
        message: `OTP sent to ${email}`,
    });
}

// ─── Resend OTP ────────────────────────────────────────────────────────────────
export async function apiResendOtp(
    email: string,
    purpose: 'register' | 'forgot-password'
): Promise<AuthApiResponse> {
    return simulate(1000, {
        success: true,
        message: `A new OTP has been sent to ${email}`,
    });
}

// ─── Verify OTP ────────────────────────────────────────────────────────────────
export async function apiVerifyOtp(
    payload: OtpVerifyPayload
): Promise<AuthApiResponse> {
    // TODO: replace with → fetch(`${BASE_URL}/auth/otp/verify`, { method:'POST', body: JSON.stringify(payload) })
    // Simulate: OTP "123456" always passes
    const correct = payload.otp === '123456';
    return simulate(1400, {
        success: correct,
        message: correct ? 'OTP verified successfully.' : 'Invalid or expired OTP. Please try again.',
    });
}

// ─── Forgot Password — Send OTP ────────────────────────────────────────────────
export async function apiForgotPassword(email: string): Promise<AuthApiResponse> {
    // TODO: replace with → fetch(`${BASE_URL}/auth/forgot-password`, { method:'POST', body: JSON.stringify({ email }) })
    return simulate(1400, {
        success: true,
        message: `Password reset OTP sent to ${email}`,
    });
}

// ─── Reset Password ────────────────────────────────────────────────────────────
export async function apiResetPassword(
    payload: ResetPasswordPayload
): Promise<AuthApiResponse> {
    // TODO: replace with → fetch(`${BASE_URL}/auth/reset-password`, { method:'POST', body: JSON.stringify(payload) })
    return simulate(1600, {
        success: true,
        message: 'Your password has been reset successfully.',
    });
}
