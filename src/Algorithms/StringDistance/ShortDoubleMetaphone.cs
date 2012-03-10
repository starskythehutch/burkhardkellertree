﻿/**
 * ShortDoubleMetaphone.cs
 * 
 * An implemenatation of Lawrence Phillips' Double Metaphone phonetic matching
 * algorithm, published in C/C++ Users Journal, June, 2000.  This implementation
 * implements Lawrence's proposed optimization, whereby four-character metaphone keys
 * are represented as four nibbles in an unsigned short.  This dramatically improves
 * storage and search efficiency.
 * 
 * This implementation was written by Adam J. Nelson (anelson@nullpointer.net).
 * It is based on the general C# implementation, also by Adam Nelson.
 * For the latest version of this implementation, implementations
 * in other languages, and links to articles I've written on the use of my various
 * Double Metaphone implementations, see:
 * http;//www.nullpointer.net/anelson/
 * 
 * Note that since this impl implements IComparable, it can be used to key associative containers,
 * thereby easily implementing phonetic matching within a simple container.  Examples of this
 * should have been included in the archive from which you obtained this file.
 * 
 * Current Version: 1.0.0
 * Revision History:
 * 	1.0.0 - ajn - First release
 * 
 * This implemenatation, and optimizations, Copyright (C) 2003, Adam J. Nelson
 * The Double Metaphone algorithm was written by Lawrence Phillips, and is 
 * Copyright (c) 1998, 1999 by Lawrence Philips.
 */

namespace Algorithms.StringDistance
{
    using System;

    /// <summary>Subclass of DoubleMetaphoneDistance, Adam Nelson's (anelson@nullpointer.net)
    ///     C# implementation of Lawrence Phillips' Double Metaphone algorithm,
    ///     published in C/C++ Users Journal, June, 2000.
    /// 
    ///     This subclass implements Lawrence's suggested optimization, whereby
    ///     four-letter metaphone keys are represented as four nibbles in an
    ///     unsigned short.  This greatly improves storage and search efficiency.</summary>
    public class ShortDoubleMetaphone : DoubleMetaphone
    {
        //Constants representing the characters in a metaphone key
        public const ushort METAPHONE_0 = 0x0E;

        public const ushort METAPHONE_A = 0x01;

        public const ushort METAPHONE_F = 0x02;

        public const ushort METAPHONE_FX = ((METAPHONE_F << 4) | METAPHONE_X);

        public const ushort METAPHONE_H = 0x03;

        /// Sentinel value, used to denote an invalid key
        public const ushort METAPHONE_INVALID_KEY = 0xffff;

        public const ushort METAPHONE_J = 0x04;

        public const ushort METAPHONE_K = 0x05;

        public const ushort METAPHONE_KL = ((METAPHONE_K << 4) | METAPHONE_L);

        public const ushort METAPHONE_KN = ((METAPHONE_K << 4) | METAPHONE_N);

        public const ushort METAPHONE_KS = ((METAPHONE_K << 4) | METAPHONE_S);

        public const ushort METAPHONE_L = 0x06;

        public const ushort METAPHONE_M = 0x07;

        public const ushort METAPHONE_N = 0x08;

        public const ushort METAPHONE_NULL = 0x00;

        public const ushort METAPHONE_P = 0x09;

        public const ushort METAPHONE_R = 0x0C;

        public const ushort METAPHONE_S = 0x0A;

        public const ushort METAPHONE_SK = ((METAPHONE_S << 4) | METAPHONE_K);

        public const ushort METAPHONE_SPACE = 0x0F;

        public const ushort METAPHONE_T = 0x0B;

        public const ushort METAPHONE_TK = ((METAPHONE_T << 4) | METAPHONE_K);

        public const ushort METAPHONE_TS = ((METAPHONE_T << 4) | METAPHONE_S);

        public const ushort METAPHONE_X = 0x0D;

        /// The ushort versions of the primary and alternate keys
        private ushort m_alternateShortKey;

        /// The ushort versions of the primary and alternate keys
        private ushort m_primaryShortKey;

        /// <summary>Default ctor, initializes to an empty string and 0 keys</summary>
        public ShortDoubleMetaphone()
        {
            this.m_primaryShortKey = this.m_alternateShortKey = 0;
        }

        /// <summary>Initializes the base class with the given word, then computes
        ///     ushort representations of the metaphone keys computed by the
        ///     base class</summary>
        /// 
        /// <param name="word">Word for which to compute metaphone keys</param>
        public ShortDoubleMetaphone(String word)
            : base(word)
        {
            this.m_primaryShortKey = metaphoneKeyToShort(this.PrimaryKey);
            if (this.AlternateKey != null)
            {
                this.m_alternateShortKey = metaphoneKeyToShort(this.AlternateKey);
            }
            else
            {
                this.m_alternateShortKey = METAPHONE_INVALID_KEY;
            }
        }

        /// <summary>The alternative metaphone key, or METAPHONE_INVALID_KEY if the current
        ///     word has no alternate key by double metaphone</summary>
        public ushort AlternateShortKey
        {
            get
            {
                return this.m_alternateShortKey;
            }
        }

        /// <summary>The primary metaphone key, represented as a ushort</summary>
        public ushort PrimaryShortKey
        {
            get
            {
                return this.m_primaryShortKey;
            }
        }

        /// <summary>Sets a new current word, computing the string and ushort representations
        ///     of the metaphone keys of the given word.
        /// 
        ///     Note that this uses the new modifier, which hides the base class
        ///     computeKeys.  The base class's computeKeys is then explicitly 
        ///     called as part of the function body.  It is important to note that
        ///     this is NOT equivalent to overriding a virtual function, in that 
        ///     polymorphism is not provided.  In this case, polymorphism is of no 
        ///     value, while the potential efficiency gained by not using virtual
        ///     methods is quite valuable.</summary>
        /// 
        /// <param name="word">New current word for which to compute metaphone keys</param>
        public new void computeKeys(String word)
        {
            base.computeKeys(word);

            this.m_primaryShortKey = metaphoneKeyToShort(this.PrimaryKey);
            this.m_alternateShortKey = this.AlternateKey != null
                                           ? metaphoneKeyToShort(this.AlternateKey)
                                           : METAPHONE_INVALID_KEY;
        }

        /// <summary>Represents a string metaphone key as a ushort</summary>
        /// 
        /// <param name="metaphoneKey">String metaphone key.  Must be four chars long; if you change 
        ///     METAPHONE_KEY_LENGTH in DoubleMetaphoneDistance, this will break.  Length
        ///     tests are not performed, for performance reasons.</param>
        /// 
        /// <returns>ushort representation of the given metahphone key</returns>
        private static ushort metaphoneKeyToShort(String metaphoneKey)
        {
            ushort result = 0;

            foreach (char t in metaphoneKey)
            {
                Char currentChar = t;
                ushort charResult;
                switch (currentChar)
                {
                    case 'A':
                        charResult = METAPHONE_A;
                        break;
                    case 'P':
                        charResult = METAPHONE_P;
                        break;
                    case 'S':
                        charResult = METAPHONE_S;
                        break;
                    case 'K':
                        charResult = METAPHONE_K;
                        break;
                    case 'X':
                        charResult = METAPHONE_X;
                        break;
                    case 'J':
                        charResult = METAPHONE_J;
                        break;
                    case 'T':
                        charResult = METAPHONE_T;
                        break;
                    case 'F':
                        charResult = METAPHONE_F;
                        break;
                    case 'N':
                        charResult = METAPHONE_N;
                        break;
                    case 'H':
                        charResult = METAPHONE_H;
                        break;
                    case 'M':
                        charResult = METAPHONE_M;
                        break;
                    case 'L':
                        charResult = METAPHONE_L;
                        break;
                    case 'R':
                        charResult = METAPHONE_R;
                        break;
                    case ' ':
                        charResult = METAPHONE_SPACE;
                        break;
                    case '\0':
                        charResult = METAPHONE_0;
                        break;
                    default:
                        charResult = 0x00; //This should never happen
                        break;
                }

                result <<= 4;
                result |= charResult;
            }
            return result;
        }
    }
}