import React, { useEffect, useRef } from 'react';
import { Loader2, CheckCircle2, XCircle, RefreshCw, Mail } from 'lucide-react';
import OtpInput from '../components/OtpInput';
import { useOtp } from '../hooks/useOtp';
import './OtpVerifyPage.css';

interface OtpVerifyPageProps {
    email: string;
    purpose: 'register' | 'forgot-password';
    onSuccess: () => void;
    onBack?: () => void;
}

const OtpVerifyPage: React.FC<OtpVerifyPageProps> = ({
    email,
    purpose,
    onSuccess,
    onBack,
}) => {
    const {
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
    } = useOtp({ email, purpose, onSuccess });

    // Auto-submit when all digits are filled
    useEffect(() => {
        if (isFilled && status === 'idle') {
            verifyOtp();
        }
    }, [isFilled, digits]);

    const firstInputRef = useRef<HTMLInputElement>(null);
    useEffect(() => {
        // auto focus first cell on mount
        setTimeout(() => {
            document.getElementById('otp-cell-0')?.focus();
        }, 100);
    }, []);

    const purposeLabel =
        purpose === 'register' ? 'registration' : 'password reset';

    return (
        <div className="otp-page">
            {/* Icon */}
            <div className="otp-page__icon-wrap">
                <div className="otp-page__icon">
                    <Mail size={32} />
                </div>
                <div className="otp-page__icon-ring" />
            </div>

            {/* Header */}
            <div className="otp-page__header">
                <h2 className="otp-page__title">Check your inbox</h2>
                <p className="otp-page__subtitle">
                    We sent a 6-digit verification code to{' '}
                    <span className="otp-page__email">{email}</span>
                </p>
                <p className="otp-page__hint">Enter the code below to verify your {purposeLabel}.</p>
            </div>

            {/* OTP Cells */}
            <div className="otp-page__input-wrap">
                <OtpInput
                    digits={digits}
                    onDigitChange={updateDigit}
                    status={status}
                    disabled={loading || status === 'success'}
                />
            </div>

            {/* Status feedback */}
            {status === 'success' && (
                <div className="otp-page__feedback otp-page__feedback--success">
                    <CheckCircle2 size={18} />
                    OTP verified successfully! Redirecting…
                </div>
            )}
            {status === 'error' && errorMsg && (
                <div className="otp-page__feedback otp-page__feedback--error">
                    <XCircle size={18} />
                    {errorMsg}
                </div>
            )}

            {/* Verify button (backup — auto-submits when filled) */}
            <button
                id="otp-verify-btn"
                className={`otp-page__submit ${loading ? 'loading' : ''} ${status === 'success' ? 'success' : ''}`}
                onClick={verifyOtp}
                disabled={!isFilled || loading || status === 'success'}
            >
                {loading ? (
                    <>
                        <Loader2 size={18} className="spin" />
                        Verifying…
                    </>
                ) : status === 'success' ? (
                    <>
                        <CheckCircle2 size={18} />
                        Verified!
                    </>
                ) : (
                    'Verify Code'
                )}
            </button>

            {/* Resend */}
            <div className="otp-page__resend">
                <span className="otp-page__resend-label">Didn't receive the code?</span>
                <button
                    id="otp-resend-btn"
                    className="otp-page__resend-btn"
                    onClick={resendOtp}
                    disabled={resendCooldown > 0 || resending || status === 'success'}
                >
                    {resending ? (
                        <>
                            <Loader2 size={14} className="spin" />
                            Sending…
                        </>
                    ) : resendCooldown > 0 ? (
                        <>
                            <RefreshCw size={14} />
                            Resend in {resendCooldown}s
                        </>
                    ) : (
                        <>
                            <RefreshCw size={14} />
                            Resend OTP
                        </>
                    )}
                </button>
            </div>

            {/* Back link */}
            {onBack && (
                <button className="otp-page__back" onClick={onBack}>
                    ← Go back
                </button>
            )}

            {/* Dev hint */}
            <div className="otp-page__dev-hint">
                💡 Demo: use <strong>123456</strong> as the OTP code
            </div>
        </div>
    );
};

export default OtpVerifyPage;
