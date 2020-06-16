namespace KleinSharp
{
#if false
#pragma once

namespace kln
{
/// \defgroup exp_log Exponential and Logarithm
/// @{
///
/// The group of rotations, translations, and screws (combined rotatation and
/// translation) is _nonlinear_. This means, given say, a Rotor $\mathbf{r}$,
/// the Rotor
/// $\frac{\mathbf{r}}{2}$ _does not_ correspond to half the rotation.
/// Similarly, for a motor $\mathbf{m}$, the motor $n \mathbf{m}$ is not $n$
/// applications of the motor $\mathbf{m}$. One way we could achieve this is
/// through exponentiation; for example, the motor $\mathbf{m}^3$ will perform
/// the screw action of $\mathbf{m}$ three times. However, repeated
/// multiplication in this fashion lacks both efficiency and numerical
/// stability.
///
/// The solution is to take the logarithm of the action which maps the action to
/// a linear space. Using `log(A)` where `A` is one of `Rotor`,
/// `translator`, or `motor`, we can apply linear scaling to `log(A)`,
/// and then re-exponentiate the result. Using this technique, `exp(n * log(A))`
/// is equivalent to $\mathbf{A}^n$.

/// Takes the principal Branch of the logarithm of the motor, returning a
/// bivector. Exponentiation of that bivector without any changes produces
/// this motor again. Scaling that bivector by $\frac{1}{n}$,
/// re-exponentiating, and taking the result to the $n$th power will also
/// produce this motor again. The logarithm presumes that the motor is
/// normalized.
public line Log(motor m) 
{
    line out;
    Detail.log(m.P1, m.p2_, out.P1, out.p2_);
    return out;
}

/// Exponentiate a line to produce a motor that posesses this line
/// as its axis. This routine will be used most often when this line is
/// produced as the logarithm of an existing Rotor, then scaled to subdivide
/// or accelerate the motor's action. The line need not be a _simple bivector_
/// for the operation to be well-defined.
public motor Exp(line l) 
{
    motor out;
    Detail.exp(l.P1, l.p2_, out.P1, out.p2_);
    return out;
}

/// Compute the logarithm of the translator, producing an ideal line axis.
/// In practice, the logarithm of a translator is simply the ideal partition
/// (without the scalar $1$).
public ideal_line Log(translator t) 
{
    ideal_line out;
    out.p2_ = t.p2_;
    return out;
}

/// Exponentiate an ideal line to produce a translation.
///
/// The exponential of an ideal line
/// $a e₀₁ + be₀₂ + ce₀₃$ is given as:
///
/// $$\exp{\left[a\ee_{01} + b\ee_{02} + c\ee_{03}\right]} = 1 +\
/// a\ee_{01} + b\ee_{02} + c\ee_{03}$$
public translator Exp(ideal_line il) 
{
    translator out;
    out.p2_ = il.p2_;
    return out;
}

/// Compute the square root of the provided translator $t$.
public translator Sqrt(translator t) 
{
    t *= 0.5f;
    return t;
}

/// Compute the square root of the provided motor $m$.
public motor Sqrt(motor m) 
{
    m.P1 = _mm_add_ss(m.P1, _mm_set_ss(1.f));
    m.normalize();
    return m;
}
/// @}
} // namespace kln#endif
#endif
}