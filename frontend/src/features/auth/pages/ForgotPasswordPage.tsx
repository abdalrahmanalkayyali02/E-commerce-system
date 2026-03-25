import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { Mail, Lock, Eye, EyeOff, ArrowRight, Loader2, KeyRound } from 'lucide-react';
import AuthLayout from '../../../pages/auth/AuthLayout';
import AuthInput from '../../../components/AuthInput';
import OtpVerifyPage from './OtpVerifyPage';
import SuccessPage from './SuccessPage';
import { useForgotPassword } from '../hooks/useForgotPassword';
import './ForgotPasswordPage.css';

const ForgotPasswordPage: React.FC = () => {
    const {
        step, setStep,
        email, setEmail,
        newPassword, setNewPassword,
        confirmPassword, setConfirmPassword,
        loading, errors, clearError,
        sendOtp, resetPassword,
        passwordStrength, PASSWORD_RULES,
    } = useForgotPassword();

    const [showPassword, setShowPassword] = useState(false);
    const [showConfirm, setShowConfirm] = useState(false);

    const strengthLabel = ['', 'Weak', 'Fair', 'Good', 'Strong'][passwordStrength];
    const strengthClass = ['', 'weak', 'fair', 'good', 'strong'][passwordStrength];

    // ── Step: Send OTP ───────────────────────────────────────────────────────────
    if (step === 'enter-email') {
        return (
            <AuthLayout
                illustrationTitle="Forgot your password?"
                illustrationSubtitle="No worries — enter your email and we'll send you a secure reset code."
            >
                <div className="forgot-page">
                    {/* Icon */}
                    <div className="forgot-page__icon-wrap">
                        <div className="forgot-page__icon">
                            <KeyRound size={32} />
                        </div>
                    </div>

                    {/* Header */}
                    <div className="forgot-page__header">
                        <h2 className="forgot-page__title">Reset Password</h2>
                        <p className="forgot-page__subtitle">
                            Enter the email address associated with your account and we'll send you a one-time code.
                        </p>
                    </div>

                    {/* General error */}
                    {errors.general && (
                        <div className="forgot-page__alert" role="alert">🚨 {errors.general}</div>
                    )}

                    {/* Form */}
                    <form
                        id="forgot-email-form"
                        onSubmit={(e) => { e.preventDefault(); sendOtp(); }}
                        noValidate
                        className="forgot-page__form"
                    >
                        <AuthInput
                            id="forgot-email"
                            name="email"
                            type="email"
                            label="Email Address"
                            placeholder="you@example.com"
                            value={email}
                            onChange={(e) => {
                                setEmail(e.target.value);
                                clearError('email');
                            }}
                            error={errors.email}
                            autoComplete="email"
                            icon={<Mail size={17} />}
                        />

                        <button
                            id="forgot-send-otp"
                            type="submit"
                            className={`forgot-page__submit ${loading ? 'loading' : ''}`}
                            disabled={loading}
                        >
                            {loading ? (
                                <><Loader2 size={18} className="spin" /> Sending OTP…</>
                            ) : (
                                <>Send Reset Code <ArrowRight size={18} /></>
                            )}
                        </button>
                    </form>

                    <p className="forgot-page__back-link">
                        Remember it?{' '}
                        <Link to="/login">Back to Sign In →</Link>
                    </p>
                </div>
            </AuthLayout>
        );
    }

    // ── Step: Verify OTP ────────────────────────────────────────────────────────
    if (step === 'verify-otp') {
        return (
            <AuthLayout
                illustrationTitle="One step away!"
                illustrationSubtitle="Enter the OTP we sent to your email to continue the reset process."
            >
                <div className="forgot-page forgot-page--otp">
                    <OtpVerifyPage
                        email={email}
                        purpose="forgot-password"
                        onSuccess={() => setStep('set-new-password')}
                        onBack={() => setStep('enter-email')}
                    />
                </div>
            </AuthLayout>
        );
    }

    // ── Step: Set New Password ──────────────────────────────────────────────────
    if (step === 'set-new-password') {
        return (
            <AuthLayout
                illustrationTitle="Create a new password"
                illustrationSubtitle="Choose a strong password that you haven't used before."
            >
                <div className="forgot-page">
                    {/* Icon */}
                    <div className="forgot-page__icon-wrap">
                        <div className="forgot-page__icon forgot-page__icon--lock">
                            <Lock size={30} />
                        </div>
                    </div>

                    {/* Header */}
                    <div className="forgot-page__header">
                        <h2 className="forgot-page__title">Set New Password</h2>
                        <p className="forgot-page__subtitle">
                            Create a strong password for your account.
                        </p>
                    </div>

                    {/* General error */}
                    {errors.general && (
                        <div className="forgot-page__alert" role="alert">🚨 {errors.general}</div>
                    )}

                    {/* Form */}
                    <form
                        id="reset-password-form"
                        onSubmit={(e) => { e.preventDefault(); resetPassword(); }}
                        noValidate
                        className="forgot-page__form"
                    >
                        <AuthInput
                            id="new-password"
                            name="newPassword"
                            type={showPassword ? 'text' : 'password'}
                            label="New Password"
                            placeholder="Min. 8 characters"
                            value={newPassword}
                            onChange={(e) => {
                                setNewPassword(e.target.value);
                                clearError('password');
                            }}
                            error={errors.password}
                            autoComplete="new-password"
                            icon={<Lock size={16} />}
                            rightElement={
                                <span
                                    role="button"
                                    aria-label="Toggle password visibility"
                                    onClick={() => setShowPassword((v) => !v)}
                                >
                                    {showPassword ? <EyeOff size={15} /> : <Eye size={15} />}
                                </span>
                            }
                        />

                        {/* Strength */}
                        {newPassword && (
                            <div className="password-strength">
                                <div className="password-strength__bars">
                                    {[1, 2, 3, 4].map((i) => (
                                        <div
                                            key={i}
                                            className={`strength-bar ${i <= passwordStrength ? `strength-bar--${strengthClass}` : ''}`}
                                        />
                                    ))}
                                </div>
                                <span className={`password-strength__label strength-label--${strengthClass}`}>
                                    {strengthLabel}
                                </span>
                            </div>
                        )}

                        {/* Rules */}
                        <div className="password-rules">
                            {PASSWORD_RULES.map((rule) => (
                                <div
                                    key={rule.label}
                                    className={`password-rule ${rule.test(newPassword) ? 'password-rule--met' : ''}`}
                                >
                                    <span className="password-rule__dot" />
                                    {rule.label}
                                </div>
                            ))}
                        </div>

                        <AuthInput
                            id="confirm-new-password"
                            name="confirmPassword"
                            type={showConfirm ? 'text' : 'password'}
                            label="Confirm New Password"
                            placeholder="Repeat your new password"
                            value={confirmPassword}
                            onChange={(e) => {
                                setConfirmPassword(e.target.value);
                                clearError('confirmPassword');
                            }}
                            error={errors.confirmPassword}
                            autoComplete="new-password"
                            icon={<Lock size={16} />}
                            rightElement={
                                <span
                                    role="button"
                                    aria-label="Toggle confirm password visibility"
                                    onClick={() => setShowConfirm((v) => !v)}
                                >
                                    {showConfirm ? <EyeOff size={15} /> : <Eye size={15} />}
                                </span>
                            }
                        />

                        <button
                            id="reset-password-submit"
                            type="submit"
                            className={`forgot-page__submit ${loading ? 'loading' : ''}`}
                            disabled={loading}
                        >
                            {loading ? (
                                <><Loader2 size={18} className="spin" /> Updating password…</>
                            ) : (
                                <>Update Password <ArrowRight size={18} /></>
                            )}
                        </button>
                    </form>
                </div>
            </AuthLayout>
        );
    }

    // ── Step: Success ───────────────────────────────────────────────────────────
    return (
        <AuthLayout
            illustrationTitle="Password changed! 🎉"
            illustrationSubtitle="You can now sign in with your new password."
        >
            <div className="forgot-page">
                <SuccessPage
                    title="Password Reset!"
                    subtitle="Your password has been updated successfully. Sign in to continue shopping."
                    ctaLabel="Sign In Now"
                    ctaHref="/login"
                />
            </div>
        </AuthLayout>
    );
};

export default ForgotPasswordPage;
