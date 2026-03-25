import React, { useState, useRef, useEffect, useCallback } from 'react';
import './PhoneInput.css';

export interface Country {
    name: string;
    code: string;   // ISO 3166-1 alpha-2
    dial: string;   // e.g. "+1"
    flag: string;   // emoji flag
}

export const COUNTRIES: Country[] = [
    { name: 'Afghanistan', code: 'AF', dial: '+93', flag: '🇦🇫' },
    { name: 'Albania', code: 'AL', dial: '+355', flag: '🇦🇱' },
    { name: 'Algeria', code: 'DZ', dial: '+213', flag: '🇩🇿' },
    { name: 'Argentina', code: 'AR', dial: '+54', flag: '🇦🇷' },
    { name: 'Australia', code: 'AU', dial: '+61', flag: '🇦🇺' },
    { name: 'Austria', code: 'AT', dial: '+43', flag: '🇦🇹' },
    { name: 'Bahrain', code: 'BH', dial: '+973', flag: '🇧🇭' },
    { name: 'Bangladesh', code: 'BD', dial: '+880', flag: '🇧🇩' },
    { name: 'Belgium', code: 'BE', dial: '+32', flag: '🇧🇪' },
    { name: 'Brazil', code: 'BR', dial: '+55', flag: '🇧🇷' },
    { name: 'Canada', code: 'CA', dial: '+1', flag: '🇨🇦' },
    { name: 'Chile', code: 'CL', dial: '+56', flag: '🇨🇱' },
    { name: 'China', code: 'CN', dial: '+86', flag: '🇨🇳' },
    { name: 'Colombia', code: 'CO', dial: '+57', flag: '🇨🇴' },
    { name: 'Czech Republic', code: 'CZ', dial: '+420', flag: '🇨🇿' },
    { name: 'Denmark', code: 'DK', dial: '+45', flag: '🇩🇰' },
    { name: 'Egypt', code: 'EG', dial: '+20', flag: '🇪🇬' },
    { name: 'Finland', code: 'FI', dial: '+358', flag: '🇫🇮' },
    { name: 'France', code: 'FR', dial: '+33', flag: '🇫🇷' },
    { name: 'Germany', code: 'DE', dial: '+49', flag: '🇩🇪' },
    { name: 'Ghana', code: 'GH', dial: '+233', flag: '🇬🇭' },
    { name: 'Greece', code: 'GR', dial: '+30', flag: '🇬🇷' },
    { name: 'Hungary', code: 'HU', dial: '+36', flag: '🇭🇺' },
    { name: 'India', code: 'IN', dial: '+91', flag: '🇮🇳' },
    { name: 'Indonesia', code: 'ID', dial: '+62', flag: '🇮🇩' },
    { name: 'Iran', code: 'IR', dial: '+98', flag: '🇮🇷' },
    { name: 'Iraq', code: 'IQ', dial: '+964', flag: '🇮🇶' },
    { name: 'Ireland', code: 'IE', dial: '+353', flag: '🇮🇪' },
    { name: 'Israel', code: 'IL', dial: '+972', flag: '🇮🇱' },
    { name: 'Italy', code: 'IT', dial: '+39', flag: '🇮🇹' },
    { name: 'Japan', code: 'JP', dial: '+81', flag: '🇯🇵' },
    { name: 'Jordan', code: 'JO', dial: '+962', flag: '🇯🇴' },
    { name: 'Kenya', code: 'KE', dial: '+254', flag: '🇰🇪' },
    { name: 'Kuwait', code: 'KW', dial: '+965', flag: '🇰🇼' },
    { name: 'Lebanon', code: 'LB', dial: '+961', flag: '🇱🇧' },
    { name: 'Libya', code: 'LY', dial: '+218', flag: '🇱🇾' },
    { name: 'Malaysia', code: 'MY', dial: '+60', flag: '🇲🇾' },
    { name: 'Mexico', code: 'MX', dial: '+52', flag: '🇲🇽' },
    { name: 'Morocco', code: 'MA', dial: '+212', flag: '🇲🇦' },
    { name: 'Netherlands', code: 'NL', dial: '+31', flag: '🇳🇱' },
    { name: 'New Zealand', code: 'NZ', dial: '+64', flag: '🇳🇿' },
    { name: 'Nigeria', code: 'NG', dial: '+234', flag: '🇳🇬' },
    { name: 'Norway', code: 'NO', dial: '+47', flag: '🇳🇴' },
    { name: 'Oman', code: 'OM', dial: '+968', flag: '🇴🇲' },
    { name: 'Pakistan', code: 'PK', dial: '+92', flag: '🇵🇰' },
    { name: 'Palestine', code: 'PS', dial: '+970', flag: '🇵🇸' },
    { name: 'Philippines', code: 'PH', dial: '+63', flag: '🇵🇭' },
    { name: 'Poland', code: 'PL', dial: '+48', flag: '🇵🇱' },
    { name: 'Portugal', code: 'PT', dial: '+351', flag: '🇵🇹' },
    { name: 'Qatar', code: 'QA', dial: '+974', flag: '🇶🇦' },
    { name: 'Romania', code: 'RO', dial: '+40', flag: '🇷🇴' },
    { name: 'Russia', code: 'RU', dial: '+7', flag: '🇷🇺' },
    { name: 'Saudi Arabia', code: 'SA', dial: '+966', flag: '🇸🇦' },
    { name: 'Singapore', code: 'SG', dial: '+65', flag: '🇸🇬' },
    { name: 'South Africa', code: 'ZA', dial: '+27', flag: '🇿🇦' },
    { name: 'South Korea', code: 'KR', dial: '+82', flag: '🇰🇷' },
    { name: 'Spain', code: 'ES', dial: '+34', flag: '🇪🇸' },
    { name: 'Sudan', code: 'SD', dial: '+249', flag: '🇸🇩' },
    { name: 'Sweden', code: 'SE', dial: '+46', flag: '🇸🇪' },
    { name: 'Switzerland', code: 'CH', dial: '+41', flag: '🇨🇭' },
    { name: 'Syria', code: 'SY', dial: '+963', flag: '🇸🇾' },
    { name: 'Taiwan', code: 'TW', dial: '+886', flag: '🇹🇼' },
    { name: 'Thailand', code: 'TH', dial: '+66', flag: '🇹🇭' },
    { name: 'Tunisia', code: 'TN', dial: '+216', flag: '🇹🇳' },
    { name: 'Turkey', code: 'TR', dial: '+90', flag: '🇹🇷' },
    { name: 'UAE', code: 'AE', dial: '+971', flag: '🇦🇪' },
    { name: 'Ukraine', code: 'UA', dial: '+380', flag: '🇺🇦' },
    { name: 'United Kingdom', code: 'GB', dial: '+44', flag: '🇬🇧' },
    { name: 'United States', code: 'US', dial: '+1', flag: '🇺🇸' },
    { name: 'Venezuela', code: 'VE', dial: '+58', flag: '🇻🇪' },
    { name: 'Vietnam', code: 'VN', dial: '+84', flag: '🇻🇳' },
    { name: 'Yemen', code: 'YE', dial: '+967', flag: '🇾🇪' },
];

interface PhoneInputProps {
    label?: string;
    value: string;          // local number only (no dial code)
    dialCode: string;       // the selected dial code e.g. "+1"
    onValueChange: (localNumber: string) => void;
    onDialChange: (country: Country) => void;
    error?: string;
    id?: string;
}

const DEFAULT_COUNTRY = COUNTRIES.find(c => c.code === 'US')!;

const PhoneInput: React.FC<PhoneInputProps> = ({
    label = 'Phone Number',
    value,
    dialCode,
    onValueChange,
    onDialChange,
    error,
    id = 'phone-input',
}) => {
    const [open, setOpen] = useState(false);
    const [search, setSearch] = useState('');
    const [focused, setFocused] = useState(false);
    const wrapRef = useRef<HTMLDivElement>(null);
    const searchRef = useRef<HTMLInputElement>(null);

    const selected = COUNTRIES.find(c => c.dial === dialCode) ?? DEFAULT_COUNTRY;

    const filtered = search.trim()
        ? COUNTRIES.filter(c =>
            c.name.toLowerCase().includes(search.toLowerCase()) ||
            c.dial.includes(search) ||
            c.code.toLowerCase().includes(search.toLowerCase())
        )
        : COUNTRIES;

    // Close dropdown on outside click
    useEffect(() => {
        const handler = (e: MouseEvent) => {
            if (wrapRef.current && !wrapRef.current.contains(e.target as Node)) {
                setOpen(false);
                setSearch('');
            }
        };
        document.addEventListener('mousedown', handler);
        return () => document.removeEventListener('mousedown', handler);
    }, []);

    // Focus search when dropdown opens
    useEffect(() => {
        if (open) setTimeout(() => searchRef.current?.focus(), 50);
    }, [open]);

    const handleSelect = useCallback((country: Country) => {
        onDialChange(country);
        setOpen(false);
        setSearch('');
    }, [onDialChange]);

    const handleNumberChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        // Allow digits, spaces, dashes, parentheses only
        const cleaned = e.target.value.replace(/[^\d\s\-()]/g, '');
        onValueChange(cleaned);
    };

    return (
        <div
            ref={wrapRef}
            className={`phone-wrap ${error ? 'phone-wrap--error' : ''} ${focused ? 'phone-wrap--focused' : ''}`}
        >
            <label className="phone-label" htmlFor={id}>{label} <span className="phone-optional">(optional)</span></label>

            <div className="phone-field">
                {/* ── Country selector trigger ── */}
                <button
                    type="button"
                    className={`phone-country-btn ${open ? 'phone-country-btn--open' : ''}`}
                    onClick={() => setOpen(v => !v)}
                    aria-haspopup="listbox"
                    aria-expanded={open}
                    tabIndex={0}
                >
                    <span className="phone-flag">{selected.flag}</span>
                    <span className="phone-dial">{selected.dial}</span>
                    <svg
                        className={`phone-chevron ${open ? 'phone-chevron--up' : ''}`}
                        width="12" height="12" viewBox="0 0 12 12" fill="none"
                    >
                        <path d="M2 4L6 8L10 4" stroke="currentColor" strokeWidth="1.8" strokeLinecap="round" strokeLinejoin="round" />
                    </svg>
                </button>

                {/* ── Divider ── */}
                <div className="phone-divider" />

                {/* ── Local number input ── */}
                <input
                    id={id}
                    type="tel"
                    className="phone-number-input"
                    placeholder="000 000 0000"
                    value={value}
                    onChange={handleNumberChange}
                    onFocus={() => setFocused(true)}
                    onBlur={() => setFocused(false)}
                    autoComplete="tel-national"
                    inputMode="tel"
                />
            </div>

            {/* ── Dropdown ── */}
            {open && (
                <div className="phone-dropdown" role="listbox">
                    {/* Search */}
                    <div className="phone-search-wrap">
                        <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                            <circle cx="11" cy="11" r="8" /><line x1="21" y1="21" x2="16.65" y2="16.65" />
                        </svg>
                        <input
                            ref={searchRef}
                            className="phone-search"
                            type="text"
                            placeholder="Search country…"
                            value={search}
                            onChange={e => setSearch(e.target.value)}
                        />
                        {search && (
                            <button className="phone-search-clear" type="button" onClick={() => setSearch('')}>✕</button>
                        )}
                    </div>

                    {/* List */}
                    <ul className="phone-list">
                        {filtered.length === 0 ? (
                            <li className="phone-list__empty">No countries found</li>
                        ) : (
                            filtered.map(country => (
                                <li
                                    key={country.code}
                                    role="option"
                                    aria-selected={country.code === selected.code}
                                    className={`phone-list__item ${country.code === selected.code ? 'phone-list__item--active' : ''}`}
                                    onClick={() => handleSelect(country)}
                                >
                                    <span className="phone-list__flag">{country.flag}</span>
                                    <span className="phone-list__name">{country.name}</span>
                                    <span className="phone-list__code">{country.dial}</span>
                                    {country.code === selected.code && (
                                        <span className="phone-list__check">✓</span>
                                    )}
                                </li>
                            ))
                        )}
                    </ul>
                </div>
            )}

            {error && <p className="phone-error">{error}</p>}
        </div>
    );
};

export default PhoneInput;
