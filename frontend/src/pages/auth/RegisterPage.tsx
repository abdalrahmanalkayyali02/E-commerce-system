import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { User, Mail, Lock, Eye, EyeOff, ArrowRight, Loader2, CheckCircle2 } from 'lucide-react';
import AuthLayout from './AuthLayout';
import AuthInput from '../../components/AuthInput';
import PhoneInput, { Country, COUNTRIES } from '../../components/PhoneInput';
import './RegisterPage.css';

interface FormState {
    firstName: string;
    lastName: string;
    email: string;
    phone: string;
    password: string;
    confirmPassword: string;
}

interface Errors {
    firstName?: string;
    lastName?: string;
    email?: string;
    phone?: string;
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

const RegisterPage: React.FC = () => {
    const navigate = useNavigate();
    const [form, setForm] = useState<FormState>({
        firstName: '', lastName: '', email: '',
        phone: '', password: '', confirmPassword: '',
    });
    const [errors, setErrors] = useState<Errors>({});
    const [showPassword, setShowPassword] = useState(false);
    const [showConfirm, setShowConfirm] = useState(false);
    const [loading, setLoading] = useState(false);
    const [agreed, setAgreed] = useState(false);
    const [step, setStep] = useState<1 | 2>(1);
    const defaultCountry = COUNTRIES.find(c => c.code === 'US')!;
    const [selectedCountry, setSelectedCountry] = useState<Country>(defaultCountry);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setForm((prev) => ({ ...prev, [name]: value }));
        if (errors[name as keyof Errors]) {
            setErrors((prev) => ({ ...prev, [name]: undefined }));
        }
    };

    const validateStep1 = (): boolean => {
        const e: Errors = {};
        if (!form.firstName.trim()) e.firstName = 'First name is required.';
        if (!form.lastName.trim()) e.lastName = 'Last name is required.';
        if (!form.email) e.email = 'Email is required.';
        else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(form.email))
            e.email = 'Enter a valid email address.';
        if (form.phone && !/^[\d\s\-()]{4,14}$/.test(form.phone))
            e.phone = 'Enter a valid local phone number (digits only).';
        setErrors(e);
        return Object.keys(e).length === 0;
    };

    const validateStep2 = (): boolean => {
        const e: Errors = {};
        if (!form.password) e.password = 'Password is required.';
        else if (form.password.length < 8) e.password = 'Password must be at least 8 characters.';
        if (!form.confirmPassword) e.confirmPassword = 'Please confirm your password.';
        else if (form.password !== form.confirmPassword) e.confirmPassword = 'Passwords do not match.';
        if (!agreed) e.general = 'You must agree to the Terms of Service.';
        setErrors(e);
        return Object.keys(e).length === 0;
    };

    const handleContinue = (e: React.FormEvent) => {
        e.preventDefault();
        if (validateStep1()) setStep(2);
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        if (!validateStep2()) return;
        setLoading(true);
        setErrors({});
        try {
            await new Promise((res) => setTimeout(res, 2000));
            navigate('/login');
        } catch {
            setErrors({ general: 'Something went wrong. Please try again.' });
        } finally {
            setLoading(false);
        }
    };

    const passwordStrength = PASSWORD_RULES.filter((r) => r.test(form.password)).length;
    const strengthLabel = ['', 'Weak', 'Fair', 'Good', 'Strong'][passwordStrength];
    const strengthClass = ['', 'weak', 'fair', 'good', 'strong'][passwordStrength];

    return (
        <AuthLayout
            illustrationTitle="Your Next Shopping Adventure Starts Here."
            illustrationSubtitle="Join millions of shoppers who trust NexCart for the best products at amazing prices."
        >
            <div className="register-page">
                {/* Header */}
                <div className="register-page__header">
                    <div className="register-page__greeting">🚀 Get started — it's free</div>
                    <h2 className="register-page__title">Create your account</h2>
                    <p className="register-page__subtitle">
                        Already have an account?{' '}
                        <Link to="/login">Sign in →</Link>
                    </p>
                </div>

                {/* Step indicator */}
                <div className="register-page__steps">
                    <div className={`step-dot ${step >= 1 ? 'step-dot--active' : ''}`}>
                        {step > 1 ? <CheckCircle2 size={14} /> : '1'}
                    </div>
                    <div className={`step-line ${step >= 2 ? 'step-line--active' : ''}`} />
                    <div className={`step-dot ${step >= 2 ? 'step-dot--active' : ''}`}>2</div>
                    <span className="step-label">Step {step} of 2 — {step === 1 ? 'Personal Info' : 'Set Password'}</span>
                </div>

                {/* ── STEP 1 ── */}
                {step === 1 && (
                    <form id="register-step1" onSubmit={handleContinue} noValidate>
                        <div className="register-page__fields">
                            <div className="register-page__row">
                                <AuthInput
                                    id="reg-firstname"
                                    name="firstName"
                                    type="text"
                                    label="First Name"
                                    placeholder="John"
                                    value={form.firstName}
                                    onChange={handleChange}
                                    error={errors.firstName}
                                    autoComplete="given-name"
                                    icon={<User size={16} />}
                                />
                                <AuthInput
                                    id="reg-lastname"
                                    name="lastName"
                                    type="text"
                                    label="Last Name"
                                    placeholder="Doe"
                                    value={form.lastName}
                                    onChange={handleChange}
                                    error={errors.lastName}
                                    autoComplete="family-name"
                                    icon={<User size={16} />}
                                />
                            </div>

                            <AuthInput
                                id="reg-email"
                                name="email"
                                type="email"
                                label="Email Address"
                                placeholder="you@example.com"
                                value={form.email}
                                onChange={handleChange}
                                error={errors.email}
                                autoComplete="email"
                                icon={<Mail size={16} />}
                            />

                            <PhoneInput
                                id="reg-phone"
                                label="Phone Number"
                                value={form.phone}
                                dialCode={selectedCountry.dial}
                                onValueChange={(num) => {
                                    setForm(prev => ({ ...prev, phone: num }));
                                    if (errors.phone) setErrors(prev => ({ ...prev, phone: undefined }));
                                }}
                                onDialChange={(country) => setSelectedCountry(country)}
                                error={errors.phone}
                            />
                        </div>

                        <button id="register-continue" type="submit" className="register-page__submit">
                            Continue
                            <ArrowRight size={18} />
                        </button>
                    </form>
                )}

                {/* ── STEP 2 ── */}
                {step === 2 && (
                    <form id="register-step2" onSubmit={handleSubmit} noValidate>
                        <div className="register-page__fields">
                            {errors.general && (
                                <div className="register-page__alert" role="alert">
                                    🚨 {errors.general}
                                </div>
                            )}

                            <AuthInput
                                id="reg-password"
                                name="password"
                                type={showPassword ? 'text' : 'password'}
                                label="Create Password"
                                placeholder="Min. 8 characters"
                                value={form.password}
                                onChange={handleChange}
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

                            {/* Password strength */}
                            {form.password && (
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

                            {/* Password rules */}
                            <div className="password-rules">
                                {PASSWORD_RULES.map((rule) => (
                                    <div key={rule.label} className={`password-rule ${rule.test(form.password) ? 'password-rule--met' : ''}`}>
                                        <span className="password-rule__dot" />
                                        {rule.label}
                                    </div>
                                ))}
                            </div>

                            <AuthInput
                                id="reg-confirm"
                                name="confirmPassword"
                                type={showConfirm ? 'text' : 'password'}
                                label="Confirm Password"
                                placeholder="Repeat your password"
                                value={form.confirmPassword}
                                onChange={handleChange}
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

                            {/* Terms */}
                            <label className="register-page__agree" htmlFor="agree-terms">
                                <input
                                    id="agree-terms"
                                    type="checkbox"
                                    checked={agreed}
                                    onChange={(e) => {
                                        setAgreed(e.target.checked);
                                        if (errors.general?.includes('Terms'))
                                            setErrors((p) => ({ ...p, general: undefined }));
                                    }}
                                />
                                <span className="checkmark" />
                                I agree to the{' '}
                                <Link to="/terms">Terms of Service</Link> and{' '}
                                <Link to="/privacy">Privacy Policy</Link>
                            </label>
                        </div>

                        {/* Buttons */}
                        <div className="register-page__btn-row">
                            <button
                                id="register-back"
                                type="button"
                                className="register-page__back"
                                onClick={() => { setStep(1); setErrors({}); }}
                            >
                                ← Back
                            </button>
                            <button
                                id="register-submit"
                                type="submit"
                                className={`register-page__submit register-page__submit--grow ${loading ? 'loading' : ''}`}
                                disabled={loading}
                            >
                                {loading ? (
                                    <><Loader2 size={18} className="spin" /> Creating account…</>
                                ) : (
                                    <>Create Account <ArrowRight size={18} /></>
                                )}
                            </button>
                        </div>
                    </form>
                )}
            </div>
        </AuthLayout>
    );
};

export default RegisterPage;
