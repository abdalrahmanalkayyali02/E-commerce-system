import type { ReactNode, ChangeEvent, SyntheticEvent } from 'react';
import { useState } from 'react';
import { motion } from 'framer-motion';
import { Send, CheckCircle } from 'lucide-react';

export function Newsletter(): ReactNode {
  const [email, setEmail] = useState<string>('');
  const [submitted, setSubmitted] = useState<boolean>(false);

  const handleSubmit = (e: SyntheticEvent<HTMLFormElement>): void => {
    e.preventDefault();
    if (email) {
      setSubmitted(true);
      setTimeout((): void => setSubmitted(false), 4000);
      setEmail('');
    }
  };

  return (
    <section className="newsletter" id="newsletter">
      <div className="newsletter__glow" />
      <motion.div
        className="newsletter__content"
        initial={{ opacity: 0, y: 40 }}
        whileInView={{ opacity: 1, y: 0 }}
        viewport={{ once: true }}
        transition={{ duration: 0.7 }}
      >
        <span className="section-tag">Stay in the Loop</span>
        <h2 className="newsletter__title">
          Get Exclusive Offers & <br />
          <span className="newsletter__title-accent">New Arrivals</span>
        </h2>
        <p className="newsletter__subtitle">
          Subscribe to our newsletter and get 15% off your first order. Be the
          first to know about new collections and exclusive deals.
        </p>

        <form
          className="newsletter__form"
          id="newsletter-form"
          onSubmit={handleSubmit}
        >
          <div className="newsletter__input-wrapper">
            <input
              type="email"
              placeholder="Enter your email address"
              className="newsletter__input"
              id="newsletter-email"
              value={email}
              onChange={(e: ChangeEvent<HTMLInputElement>): void => setEmail(e.target.value)}
              required
            />
            <button
              type="submit"
              className="newsletter__submit"
              id="newsletter-submit"
            >
              {submitted ? (
                <>
                  <CheckCircle size={18} />
                  Subscribed!
                </>
              ) : (
                <>
                  <Send size={18} />
                  Subscribe
                </>
              )}
            </button>
          </div>
        </form>

        <p className="newsletter__privacy">
          By subscribing, you agree to our Privacy Policy. Unsubscribe anytime.
        </p>
      </motion.div>
    </section>
  );
}
