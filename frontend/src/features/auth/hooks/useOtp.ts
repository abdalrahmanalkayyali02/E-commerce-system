import { useState, useCallback, useEffect, useRef } from 'react';
import { apiVerifyOtp, apiResendOtp } from '../api/auth.api';

const OTP_LENGTH = 6;
const RESEND_COOLDOWN = 60; // seconds

interface UseOtpOptions {
    email: string;
    purpose: 'register' | 'forgot-password';
    onSuccess: () => void;
}

export function useOtp({ email, purpose, onSuccess }: UseOtpOptions) {
    const [digits, setDigits] = useState<string[]>(Array(OTP_LENGTH).fill(''));
    const [loading, setLoading] = useState(false);
    const [resending, setResending] = useState(false);
    const [status, setStatus] = useState<'idle' | 'success' | 'error'>('idle');
    const [errorMsg, setErrorMsg] = useState('');
    const [resendCooldown, setResendCooldown] = useState(0);
    const intervalRef = useRef<ReturnType<typeof setInterval> | null>(null);

    // Cooldown timer
    const startCooldown = useCallback(() => {
        setResendCooldown(RESEND_COOLDOWN);
        if (intervalRef.current) clearInterval(intervalRef.current);
        intervalRef.current = setInterval(() => {
            setResendCooldown((prev) => {
                if (prev <= 1) {
                    clearInterval(intervalRef.current!);
                    return 0;
                }
                return prev - 1;
            });
        }, 1000);
    }, []);

    useEffect(() => {
        // Start cooldown immediately (OTP was just sent)
        startCooldown();
        return () => {
            if (intervalRef.current) clearInterval(intervalRef.current);
        };
    }, []);

    const otp = digits.join('');
    const isFilled = otp.length === OTP_LENGTH && digits.every((d) => d !== '');

    const updateDigit = useCallback((index: number, value: string) => {
        setDigits((prev) => {
            const next = [...prev];
            next[index] = value;
            return next;
        });
        // Clear previous error when user types
        setStatus('idle');
        setErrorMsg('');
    }, []);

    const clearDigits = useCallback(() => {
        setDigits(Array(OTP_LENGTH).fill(''));
        setStatus('idle');
        setErrorMsg('');
    }, []);

    const verifyOtp = useCallback(async () => {
        if (!isFilled) return;
        setLoading(true);
        setStatus('idle');
        try {
            const res = await apiVerifyOtp({ email, otp, purpose });
            if (res.success) {
                setStatus('success');
                setTimeout(onSuccess, 1500);
            } else {
                setStatus('error');
                setErrorMsg(res.message);
                // shake + clear after a moment
                setTimeout(clearDigits, 1200);
            }
        } catch {
            setStatus('error');
            setErrorMsg('Network error. Please try again.');
            setTimeout(clearDigits, 1200);
        } finally {
            setLoading(false);
        }
    }, [email, otp, purpose, isFilled, onSuccess, clearDigits]);

    const resendOtp = useCallback(async () => {
        if (resendCooldown > 0 || resending) return;
        setResending(true);
        setStatus('idle');
        setErrorMsg('');
        try {
            await apiResendOtp(email, purpose);
            startCooldown();
            clearDigits();
        } catch {
            setErrorMsg('Failed to resend OTP. Please try again.');
        } finally {
            setResending(false);
        }
    }, [email, purpose, resendCooldown, resending, clearDigits, startCooldown]);

    return {
        digits,
        isFilled,
        loading,
        resending,
        status,
        errorMsg,
        resendCooldown,
        updateDigit,
        verifyOtp,
        resendOtp,
        clearDigits,
    };
}
