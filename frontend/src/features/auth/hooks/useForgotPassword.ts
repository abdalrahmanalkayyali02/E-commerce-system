import { useState, useCallback } from 'react';
import { apiForgotPassword, apiVerifyOtp, apiResetPassword } from '../api/auth.api';
import { ForgotPasswordStep } from '../types/auth.types';

interface ForgotErrors {
    email?: string;
    otp?: string;
    password?: string;
    confirmPassword?: string;
    general?: string;
}

const PASSWORD_RULES = [
    { label: 'At least 8 characters', test: (p: string) => p.length >= 8 },
    { label: 'One uppercase letter', test: (p: string) => /[A-Z]/.test(p) },
    { label: 'One number', test: (p: string) => /\d/.test(p) },
    { label: 'One special character', test: (p: string) => /[!@#$%^&*]/.test(p) },
];

export function useForgotPassword() {
    const [step, setStep] = useState<ForgotPasswordStep>('enter-email');
    const [email, setEmail] = useState('');
    const [otp, setOtp] = useState('');
    const [newPassword, setNewPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [loading, setLoading] = useState(false);
    const [errors, setErrors] = useState<ForgotErrors>({});

    const clearError = useCallback((field: keyof ForgotErrors) => {
        setErrors((prev) => ({ ...prev, [field]: undefined }));
    }, []);

    // ── Step 1: Send OTP ────────────────────────────────────────────────────────
    const sendOtp = useCallback(async () => {
        const e: ForgotErrors = {};
        if (!email) e.email = 'Email is required.';
        else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email))
            e.email = 'Enter a valid email address.';
        if (Object.keys(e).length > 0) { setErrors(e); return; }

        setLoading(true);
        setErrors({});
        try {
            const res = await apiForgotPassword(email);
            if (res.success) {
                setStep('verify-otp');
            } else {
                setErrors({ general: res.message });
            }
        } catch {
            setErrors({ general: 'Something went wrong. Please try again.' });
        } finally {
            setLoading(false);
        }
    }, [email]);

    // ── Step 3: Reset Password ──────────────────────────────────────────────────
    const resetPassword = useCallback(async () => {
        const e: ForgotErrors = {};
        if (!newPassword) e.password = 'Password is required.';
        else if (newPassword.length < 8) e.password = 'Password must be at least 8 characters.';
        if (!confirmPassword) e.confirmPassword = 'Please confirm your password.';
        else if (newPassword !== confirmPassword) e.confirmPassword = 'Passwords do not match.';
        if (Object.keys(e).length > 0) { setErrors(e); return; }

        setLoading(true);
        setErrors({});
        try {
            const res = await apiResetPassword({ email, otp, newPassword, confirmPassword });
            if (res.success) {
                setStep('success');
            } else {
                setErrors({ general: res.message });
            }
        } catch {
            setErrors({ general: 'Something went wrong. Please try again.' });
        } finally {
            setLoading(false);
        }
    }, [email, otp, newPassword, confirmPassword]);

    const passwordStrength = PASSWORD_RULES.filter((r) => r.test(newPassword)).length;

    return {
        step,
        setStep,
        email,
        setEmail,
        otp,
        setOtp,
        newPassword,
        setNewPassword,
        confirmPassword,
        setConfirmPassword,
        loading,
        errors,
        setErrors,
        clearError,
        sendOtp,
        resetPassword,
        passwordStrength,
        PASSWORD_RULES,
    };
}
