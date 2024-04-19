
def calculate_factorial(n):
    """Calculate the factorial of a given number."""
    if n == 0:
        return 1
    else:
        return n * calculate_factorial(n - 1)

def main():
    print("Factorial Calculator")
    try:
        number = int(input("Enter a non-negative integer: "))
        if number < 0:
            raise ValueError("Please enter a non-negative integer.")
        factorial = calculate_factorial(number)
        print(f"The factorial of {number} is {factorial}.")
    except ValueError as e:
        print(f"Invalid input: {e}")

if __name__ == "__main__":
    main()
