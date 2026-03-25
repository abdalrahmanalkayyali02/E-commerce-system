import React, { useRef, KeyboardEvent, ClipboardEvent } from 'react';
import './OtpInput.css';

interface OtpInputProps {
    digits: string[];
    onDigitChange: (index: number, value: string) => void;
    status: 'idle' | 'success' | 'error';
    disabled?: boolean;
}

const OtpInput: React.FC<OtpInputProps> = ({ digits, onDigitChange, status, disabled }) => {
    const inputRefs = useRef<(HTMLInputElement | null)[]>([]);

    const focusCell = (index: number) => {
        inputRefs.current[index]?.focus();
    };

    const handleKeyDown = (e: KeyboardEvent<HTMLInputElement>, index: number) => {
        if (e.key === 'Backspace') {
            if (digits[index]) {
                onDigitChange(index, '');
            } else if (index > 0) {
                focusCell(index - 1);
                onDigitChange(index - 1, '');
            }
        } else if (e.key === 'ArrowLeft' && index > 0) {
            focusCell(index - 1);
        } else if (e.key === 'ArrowRight' && index < digits.length - 1) {
            focusCell(index + 1);
        }
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>, index: number) => {
        const val = e.target.value.replace(/\D/g, '').slice(-1);
        onDigitChange(index, val);
        if (val && index < digits.length - 1) {
            focusCell(index + 1);
        }
    };

    const handlePaste = (e: ClipboardEvent<HTMLInputElement>) => {
        e.preventDefault();
        const pasted = e.clipboardData.getData('text').replace(/\D/g, '').slice(0, digits.length);
        pasted.split('').forEach((char, i) => {
            onDigitChange(i, char);
        });
        const nextIndex = Math.min(pasted.length, digits.length - 1);
        focusCell(nextIndex);
    };

    const statusClass =
        status === 'success'
            ? 'otp-input--success'
            : status === 'error'
                ? 'otp-input--error'
                : '';

    return (
        <div className={`otp-input ${statusClass}`} role="group" aria-label="OTP input">
            {digits.map((digit, i) => (
                <React.Fragment key={i}>
                    <input
                        ref={(el) => { inputRefs.current[i] = el; }}
                        id={`otp-cell-${i}`}
                        className={`otp-cell ${digit ? 'otp-cell--filled' : ''}`}
                        type="text"
                        inputMode="numeric"
                        pattern="[0-9]*"
                        maxLength={1}
                        value={digit}
                        disabled={disabled}
                        autoComplete="one-time-code"
                        onChange={(e) => handleChange(e, i)}
                        onKeyDown={(e) => handleKeyDown(e, i)}
                        onPaste={handlePaste}
                        onFocus={(e) => e.target.select()}
                        aria-label={`OTP digit ${i + 1}`}
                    />
                    {i === 2 && <span className="otp-separator">—</span>}
                </React.Fragment>
            ))}
        </div>
    );
};

export default OtpInput;
