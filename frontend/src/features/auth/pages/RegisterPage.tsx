import React, { useRef } from 'react';
import { Link } from 'react-router-dom';
import {
    User, Mail, Lock, Eye, EyeOff, ArrowRight, Loader2,
    CheckCircle2, MapPin, Store, Camera, ShoppingBag,
} from 'lucide-react';
import AuthLayout from '../../../pages/auth/AuthLayout';
import AuthInput from '../../../components/AuthInput';
import PhoneInput, { Country, COUNTRIES } from '../../../components/PhoneInput';
import OtpVerifyPage from './OtpVerifyPage';
import SuccessPage from './SuccessPage';
import { useRegister } from '../hooks/useRegister';
import { UserRole } from '../types/auth.types';
// shared base styles
import '../../../pages/auth/RegisterPage.css';
import './RegisterPage.css';

// ── Role card data ─────────────────────────────────────────────────────────────
const ROLES: { role: UserRole; label: string; description: string; icon: React.ReactNode }[] = [
    {
        role: 'customer',
        label: 'Customer',
        description: 'Shop from thousands of sellers and get the best deals.',
        icon: <ShoppingBag size={32} strokeWidth={1.5} />,
    },
    {
        role: 'seller',
        label: 'Seller',
        description: 'List your products and grow your business on NexCart.',
        icon: <Store size={32} strokeWidth={1.5} />,
    },
];

const RegisterPage: React.FC = () => {
    const {
        step, setStep,
        form, errors, loading, agreed, setAgreed,
        updateField, clearFieldError,
        selectRole, goToRoleDetails, goToPasswordStep, submitRegister,
        passwordStrength, PASSWORD_RULES,
    } = useRegister();

    const [showPassword, setShowPassword] = React.useState(false);
    const [showConfirm, setShowConfirm] = React.useState(false);
    const defaultCountry = COUNTRIES.find((c) => c.code === 'US')!;
    const [selectedCountry, setSelectedCountry] = React.useState<Country>(defaultCountry);

    const profileRef = useRef<HTMLInputElement>(null);
    const shopPhotoRef = useRef<HTMLInputElement>(null);
    const verificationDocRef = useRef<HTMLInputElement>(null);

    const strengthLabel = ['', 'Weak', 'Fair', 'Good', 'Strong'][passwordStrength];
    const strengthClass = ['', 'weak', 'fair', 'good', 'strong'][passwordStrength];

    // ── Step indicator ─────────────────────────────────────────────────────────
    const STEPS = ['Role', 'Personal Info', 'Details', 'Password'];
    const stepIndex: Record<string, number> = {
        'select-role': 0,
        'personal-info': 1,
        'role-details': 2,
        'set-password': 3,
    };
    const currentIdx = stepIndex[step] ?? 0;

    const renderStepIndicator = () => (
        <div className="register-page__steps">
            {STEPS.map((label, i) => (
                <React.Fragment key={label}>
                    <div className={`step-dot ${currentIdx > i ? 'step-dot--done' : ''} ${currentIdx === i ? 'step-dot--active' : ''}`}>
                        {currentIdx > i ? <CheckCircle2 size={13} /> : i + 1}
                    </div>
                    {i < STEPS.length - 1 && (
                        <div className={`step-line ${currentIdx > i ? 'step-line--active' : ''}`} />
                    )}
                </React.Fragment>
            ))}
            <span className="step-label">{STEPS[currentIdx]}</span>
        </div>
    );

    // ── Photo preview helper ───────────────────────────────────────────────────
    const photoPreview = (file: File | null) =>
        file ? URL.createObjectURL(file) : null;

    // ── OTP / Success screens ──────────────────────────────────────────────────
    if (step === 'verify-otp') {
        return (
            <AuthLayout
                illustrationTitle="Almost there!"
                illustrationSubtitle="Verify your email to complete your NexCart account setup."
            >
                <div className="register-page">
                    <OtpVerifyPage
                        email={form.email}
                        purpose="register"
                        onSuccess={() => setStep('success')}
                        onBack={() => setStep('set-password')}
                    />
                </div>
            </AuthLayout>
        );
    }

    if (step === 'success') {
        return (
            <AuthLayout
                illustrationTitle="Welcome to NexCart! 🎉"
                illustrationSubtitle="Your account is all set. Start exploring amazing deals today."
            >
                <div className="register-page">
                    <SuccessPage
                        title="Account Created!"
                        subtitle={`Welcome aboard, ${form.firstName}! Your account has been successfully verified.`}
                        ctaLabel="Go to Sign In"
                        ctaHref="/login"
                    />
                </div>
            </AuthLayout>
        );
    }

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
                {renderStepIndicator()}

                {/* ══════════════════════════════════════════════════════════════
                    STEP 0 — Select Role
                ══════════════════════════════════════════════════════════════ */}
                {step === 'select-role' && (
                    <div className="role-select">
                        <p className="role-select__hint">How will you use NexCart?</p>
                        <div className="role-select__cards">
                            {ROLES.map(({ role, label, description, icon }) => (
                                <button
                                    key={role}
                                    id={`role-${role}`}
                                    type="button"
                                    className="role-card"
                                    onClick={() => selectRole(role)}
                                >
                                    <div className="role-card__icon">{icon}</div>
                                    <div className="role-card__body">
                                        <span className="role-card__label">{label}</span>
                                        <span className="role-card__desc">{description}</span>
                                    </div>
                                    <ArrowRight size={18} className="role-card__arrow" />
                                </button>
                            ))}
                        </div>
                    </div>
                )}

                {/* ══════════════════════════════════════════════════════════════
                    STEP 1 — Personal Info
                ══════════════════════════════════════════════════════════════ */}
                {step === 'personal-info' && (
                    <form
                        id="register-step1"
                        onSubmit={(e) => { e.preventDefault(); goToRoleDetails(); }}
                        noValidate
                    >
                        <div className="register-page__fields">
                            {/* Profile photo (optional) */}
                            <div className="photo-upload">
                                <button
                                    type="button"
                                    className="photo-upload__circle"
                                    onClick={() => profileRef.current?.click()}
                                    title="Upload profile photo"
                                >
                                    {form.profilePhoto ? (
                                        <img
                                            src={photoPreview(form.profilePhoto)!}
                                            alt="Profile preview"
                                            className="photo-upload__preview"
                                        />
                                    ) : (
                                        <Camera size={24} />
                                    )}
                                </button>
                                <div className="photo-upload__info">
                                    <span className="photo-upload__label">Profile Photo</span>
                                    <span className="photo-upload__hint">Optional · JPG, PNG · Max 5 MB</span>
                                </div>
                                <input
                                    ref={profileRef}
                                    type="file"
                                    accept="image/*"
                                    style={{ display: 'none' }}
                                    onChange={(e) => updateField('profilePhoto', e.target.files?.[0] ?? null)}
                                />
                            </div>

                            {/* Name row */}
                            <div className="register-page__row">
                                <AuthInput
                                    id="reg-firstname"
                                    name="firstName"
                                    type="text"
                                    label="First Name"
                                    placeholder="John"
                                    value={form.firstName}
                                    onChange={(e) => updateField('firstName', e.target.value)}
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
                                    onChange={(e) => updateField('lastName', e.target.value)}
                                    error={errors.lastName}
                                    autoComplete="family-name"
                                    icon={<User size={16} />}
                                />
                            </div>

                            {/* Username */}
                            <AuthInput
                                id="reg-username"
                                name="username"
                                type="text"
                                label="Username"
                                placeholder="john_doe"
                                value={form.username}
                                onChange={(e) => updateField('username', e.target.value)}
                                error={errors.username}
                                autoComplete="username"
                                icon={<span style={{ fontSize: 14, fontWeight: 700, color: 'var(--text-muted)' }}>@</span>}
                            />

                            {/* Email */}
                            <AuthInput
                                id="reg-email"
                                name="email"
                                type="email"
                                label="Email Address"
                                placeholder="you@example.com"
                                value={form.email}
                                onChange={(e) => updateField('email', e.target.value)}
                                error={errors.email}
                                autoComplete="email"
                                icon={<Mail size={16} />}
                            />

                            {/* Phone */}
                            <PhoneInput
                                id="reg-phone"
                                label="Phone Number"
                                value={form.phone}
                                dialCode={selectedCountry.dial}
                                onValueChange={(num) => {
                                    updateField('phone', num);
                                    clearFieldError('phone');
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

                {/* ══════════════════════════════════════════════════════════════
                    STEP 2 — Role-specific Details
                ══════════════════════════════════════════════════════════════ */}
                {step === 'role-details' && (
                    <form
                        id="register-step2"
                        onSubmit={(e) => { e.preventDefault(); goToPasswordStep(); }}
                        noValidate
                    >
                        <div className="register-page__fields">
                            {/* ── CUSTOMER: Address ── */}
                            {form.role === 'customer' && (
                                <AuthInput
                                    id="reg-address"
                                    name="address"
                                    type="text"
                                    label="Delivery Address"
                                    placeholder="123 Main St, City, Country"
                                    value={form.address}
                                    onChange={(e) => updateField('address', e.target.value)}
                                    error={errors.address}
                                    autoComplete="street-address"
                                    icon={<MapPin size={16} />}
                                />
                            )}

                            {/* ── SELLER: Shop Name + Shop Photo ── */}
                            {form.role === 'seller' && (
                                <>
                                    <AuthInput
                                        id="reg-shopname"
                                        name="shopName"
                                        type="text"
                                        label="Shop Name"
                                        placeholder="My Awesome Store"
                                        value={form.shopName}
                                        onChange={(e) => updateField('shopName', e.target.value)}
                                        error={errors.shopName}
                                        autoComplete="organization"
                                        icon={<Store size={16} />}
                                    />

                                    {/* Shop photo */}
                                    <div className="photo-upload">
                                        <button
                                            type="button"
                                            className="photo-upload__circle photo-upload__circle--shop"
                                            onClick={() => shopPhotoRef.current?.click()}
                                            title="Upload shop photo"
                                        >
                                            {form.shopPhoto ? (
                                                <img
                                                    src={photoPreview(form.shopPhoto)!}
                                                    alt="Shop preview"
                                                    className="photo-upload__preview"
                                                />
                                            ) : (
                                                <Store size={24} />
                                            )}
                                        </button>
                                        <div className="photo-upload__info">
                                            <span className="photo-upload__label">Shop Photo</span>
                                            <span className="photo-upload__hint">Optional · JPG, PNG · Max 5 MB</span>
                                        </div>
                                        <input
                                            ref={shopPhotoRef}
                                            type="file"
                                            accept="image/*"
                                            style={{ display: 'none' }}
                                            onChange={(e) => updateField('shopPhoto', e.target.files?.[0] ?? null)}
                                        />
                                    </div>

                                    {/* Verification document */}
                                    <div className={`doc-upload ${errors.verificationDoc ? 'doc-upload--error' : ''}`}>
                                        <div className="doc-upload__header">
                                            <span className="doc-upload__title">
                                                Seller Verification Document
                                                <span className="doc-upload__required">Required</span>
                                            </span>
                                            <span className="doc-upload__hint">
                                                Trade licence, commercial registration, or national ID
                                            </span>
                                        </div>
                                        <button
                                            type="button"
                                            className="doc-upload__btn"
                                            onClick={() => verificationDocRef.current?.click()}
                                        >
                                            {form.verificationDoc ? (
                                                <>
                                                    <CheckCircle2 size={18} className="doc-upload__icon doc-upload__icon--done" />
                                                    <span className="doc-upload__filename">{form.verificationDoc.name}</span>
                                                    <span className="doc-upload__change">Change</span>
                                                </>
                                            ) : (
                                                <>
                                                    <span className="doc-upload__icon doc-upload__icon--idle">📄</span>
                                                    <span className="doc-upload__cta">Click to upload file</span>
                                                    <span className="doc-upload__formats">PDF · JPG · PNG · Max 10 MB</span>
                                                </>
                                            )}
                                        </button>
                                        {errors.verificationDoc && (
                                            <span className="doc-upload__err">{errors.verificationDoc}</span>
                                        )}
                                        <input
                                            ref={verificationDocRef}
                                            type="file"
                                            accept=".pdf,image/*"
                                            style={{ display: 'none' }}
                                            onChange={(e) => {
                                                updateField('verificationDoc', e.target.files?.[0] ?? null);
                                                clearFieldError('verificationDoc');
                                            }}
                                        />
                                    </div>
                                </>
                            )}
                        </div>

                        <div className="register-page__btn-row">
                            <button
                                id="register-back-2"
                                type="button"
                                className="register-page__back"
                                onClick={() => setStep('personal-info')}
                            >
                                ← Back
                            </button>
                            <button id="register-continue-2" type="submit" className="register-page__submit register-page__submit--grow">
                                Continue
                                <ArrowRight size={18} />
                            </button>
                        </div>
                    </form>
                )}

                {/* ══════════════════════════════════════════════════════════════
                    STEP 3 — Set Password
                ══════════════════════════════════════════════════════════════ */}
                {step === 'set-password' && (
                    <form
                        id="register-step3"
                        onSubmit={(e) => { e.preventDefault(); submitRegister(); }}
                        noValidate
                    >
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
                                onChange={(e) => updateField('password', e.target.value)}
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
                                    <div
                                        key={rule.label}
                                        className={`password-rule ${rule.test(form.password) ? 'password-rule--met' : ''}`}
                                    >
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
                                onChange={(e) => updateField('confirmPassword', e.target.value)}
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
                                            clearFieldError('general');
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
                                id="register-back-3"
                                type="button"
                                className="register-page__back"
                                onClick={() => setStep('role-details')}
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
