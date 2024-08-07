
document.addEventListener('DOMContentLoaded', function () {
    const buttons = document.querySelectorAll('.dropdown-button');
    const dropdowns = document.querySelectorAll('.dropdown');

    buttons.forEach(button => {
        button.addEventListener('click', function (event) {
            event.stopPropagation();
            const targetDropdownId = this.id.replace('-button', '');
            const targetDropdown = document.getElementById(targetDropdownId);

            dropdowns.forEach(dropdown => {
                if (dropdown !== targetDropdown) {
                    dropdown.classList.remove('active');
                    dropdown.classList.add('hidden');
                }
            });

            if (targetDropdown.classList.contains('hidden')) {
                targetDropdown.classList.remove('hidden');
                targetDropdown.classList.add('active');
            } else {
                targetDropdown.classList.remove('active');
                targetDropdown.classList.add('hidden');
            }
        });
    });

    document.addEventListener('click', function () {
        dropdowns.forEach(dropdown => {
            dropdown.classList.remove('active');
            dropdown.classList.add('hidden');
        });
    });

    dropdowns.forEach(dropdown => {
        dropdown.addEventListener('click', function (event) {
            event.stopPropagation();
        });
    });
    if (window.innerWidth <= 768) {
        var slider = tns({
            container: '.nav-menu-slider',
            items: 5,
            slideBy: 'page',
            nav: false,
            controls: false,
            autoplay: false,
            autoplayButtonOutput: false,
            mouseDrag: true,
            speed: 600,
            swipeAngle: false,
            loop: false,
            edgePadding: 10,
        });
    }
});
function toggleDiv(button) {
    const div = button.nextElementSibling;
    const svg = button.querySelector('svg');
    div.classList.toggle('expanded');
    svg.classList.toggle('rotate-180');
}

document.addEventListener('DOMContentLoaded', (event) => {
    let timerElement = document.querySelector('.fivesecondtimer');
    let time = 5;

    let countdown = setInterval(() => {
        if (time > 0) {
            timerElement.textContent = time;
            time--;
        } else {
            timerElement.textContent = "0";
            clearInterval(countdown);
        }
    }, 1000);
});

document.querySelector('.custom-mobile-sidebar-list-button').addEventListener('click', function () {
    const sidebarList = document.querySelector('.custom-mobile-user-sidebar-list');
    sidebarList.classList.toggle('show');
});

document.getElementById('fatura-option-1').addEventListener('change', function () {
    var checkbox2 = document.getElementById('IsInvoiceAddress');
    if (this.checked) {
        checkbox2.checked = false;
        document.getElementById('invoice-address').classList.add('hidden');
    } else if (!checkbox2.checked) {
        this.checked = true;
    }
    toggleInvoiceArea();
});

document.getElementById('IsInvoiceAddress').addEventListener('change', function () {
    var checkbox1 = document.getElementById('fatura-option-1');
    if (this.checked) {
        checkbox1.checked = false;
        document.getElementById('invoice-address').classList.remove('hidden');
    } else if (!checkbox1.checked) {
        this.checked = true;
    } else {
        document.getElementById('invoice-address').classList.add('hidden');
    }
    toggleInvoiceArea();
});

function toggleInvoiceArea() {
    var checkbox1 = document.getElementById('fatura-option-1');
    var checkbox2 = document.getElementById('IsInvoiceAddress');
    var invoiceArea = document.getElementById('invoice-area');

    if (checkbox1.checked || checkbox2.checked) {
        invoiceArea.classList.remove('hidden');
    }
}
document.getElementById('option-person').addEventListener('change', function () {
    if (this.checked) {
        document.getElementById('option-tc-citizen').checked = true;
        document.getElementById('tc-citizen-section').style.display = 'flex';
        document.getElementById('foreign-citizen-section').style.display = 'none';
        document.getElementById('person-section').style.display = 'flex';
        document.getElementById('company-section').style.display = 'none';
        document.getElementById('option-tc-citizen-label').style.backgroundColor = '#1c64f2';
        document.getElementById('option-tc-citizen-label').style.borderColor = '#1c64f2';
        document.getElementById('option-company-label').style.backgroundColor = 'white';
        document.getElementById('option-company-label').style.borderColor = '#e5e7eb';
        document.getElementById('option-foreign-citizen-label').style.backgroundColor = 'white';
        document.getElementById('option-person-label').style.backgroundColor = '#1c64f2';
        document.getElementById('option-foreign-citizen-label').style.borderColor = '#e5e7eb';
        document.getElementById('option-person-label').style.borderColor = '#1c64f2';

    }
});
document.getElementById('option-company').addEventListener('change', function () {
    if (this.checked) {
        document.getElementById('tc-citizen-section').style.display = 'none';
        document.getElementById('foreign-citizen-section').style.display = 'none';
        document.getElementById('person-section').style.display = 'none';
        document.getElementById('option-person-label').style.backgroundColor = 'white';
        document.getElementById('option-company-label').style.backgroundColor = '#1c64f2';
        document.getElementById('option-company-label').style.borderColor = '#1c64f2';
        document.getElementById('option-person-label').style.borderColor = '#e5e7eb';
        document.getElementById('company-section').style.display = 'flex';
    }
});
document.getElementById('option-tc-citizen').addEventListener('change', function () {
    if (this.checked) {
        document.getElementById('tc-citizen-section').style.display = 'flex';
        document.getElementById('option-tc-citizen-label').style.backgroundColor = '#1c64f2';
        document.getElementById('option-foreign-citizen-label').style.backgroundColor = 'white';
        document.getElementById('option-foreign-citizen-label').style.borderColor = '#e5e7eb';
        document.getElementById('option-tc-citizen-label').style.borderColor = '#1c64f2';
        document.getElementById('foreign-citizen-section').style.display = 'none';
    }
});
document.getElementById('option-foreign-citizen').addEventListener('change', function () {
    if (this.checked) {
        document.getElementById('tc-citizen-section').style.display = 'none';
        document.getElementById('option-tc-citizen-label').style.backgroundColor = 'white';
        document.getElementById('option-tc-citizen-label').style.borderColor = '#e5e7eb';
        document.getElementById('option-foreign-citizen-label').style.backgroundColor = '#1c64f2';
        document.getElementById('option-foreign-citizen-label').style.borderColor = '#1c64f2';
        document.getElementById('foreign-citizen-section').style.display = 'flex';
    }
});
document.getElementById('option-e-fatura-evet').addEventListener('change', function () {
    if (this.checked) {
        document.getElementById('option-tc-citizen').checked = true;
        document.getElementById('option-e-fatura-evet-label').style.backgroundColor = '#1c64f2';
        document.getElementById('option-e-fatura-hayir-label').style.backgroundColor = 'white';
        document.getElementById('option-e-fatura-evet-label').style.borderColor = '#1c64f2';
        document.getElementById('option-e-fatura-hayir-label').style.borderColor = '#e5e7eb';

    }
});
document.getElementById('option-e-fatura-hayir').addEventListener('change', function () {
    if (this.checked) {
        document.getElementById('option-tc-citizen').checked = true;
        document.getElementById('option-e-fatura-evet-label').style.backgroundColor = 'white';
        document.getElementById('option-e-fatura-evet-label').style.borderColor = '#e5e7eb';
        document.getElementById('option-e-fatura-hayir-label').style.borderColor = '#1c64f2';
        document.getElementById('option-e-fatura-hayir-label').style.backgroundColor = '#1c64f2';

    }
});

document.addEventListener('DOMContentLoaded', function () {
    function validateInput(inputField, charCount, errorMessage, minLength, maxLength, minMessage, maxMessage) {
        inputField.addEventListener('input', function () {
            const length = inputField.value.replace(/-/g, '').length;
            charCount.textContent = `${length}/${maxLength}`;

            if (length === 0) {
                inputField.classList.add('border-gray-300');
                inputField.classList.remove('border-red-500');
                errorMessage.style.display = 'none';
            } else if (length < minLength || length > maxLength) {
                inputField.classList.add('border-red-500');
                inputField.classList.remove('border-gray-300');

                if (length < minLength) {
                    errorMessage.textContent = minMessage;
                } else if (length > maxLength) {
                    errorMessage.textContent = maxMessage;
                }

                errorMessage.style.display = 'block';
                setTimeout(() => errorMessage.style.display = 'none', 4000);
            } else {
                inputField.classList.add('border-gray-300');
                inputField.classList.remove('border-red-500');
                errorMessage.style.display = 'none';
            }
        });
    }

    const userNameField = document.getElementById('add-address-user-name');
    const userNameCharCount = document.getElementById('char-count-user-name');
    const userNameErrorMessage = document.getElementById('error-message-user-name');
    validateInput(userNameField, userNameCharCount, userNameErrorMessage, 3, 30, 'En az 3 karakter girilmelidir.', 'En fazla 30 karakter girilmelidir.');

    const userPhoneField = document.getElementById('add-address-user-phone');
    const userPhoneCharCount = document.getElementById('char-count-user-phone');
    const userPhoneErrorMessage = document.getElementById('error-message-user-phone');
    userPhoneField.addEventListener('input', function () {
        userPhoneField.value = userPhoneField.value.replace(/[^0-9]/g, '').slice(0, 10);
        formatPhoneNumber(userPhoneField);
        validatePhoneInput(userPhoneField, userPhoneCharCount, userPhoneErrorMessage);
    });

    const altPhoneField = document.getElementById('add-address-alternative-phone');
    const altPhoneCharCount = document.getElementById('char-count-alternative-phone');
    const altPhoneErrorMessage = document.getElementById('error-message-alternative-phone');
    altPhoneField.addEventListener('input', function () {
        altPhoneField.value = altPhoneField.value.replace(/[^0-9]/g, '').slice(0, 10);
        formatPhoneNumber(altPhoneField);
        validatePhoneInput(altPhoneField, altPhoneCharCount, altPhoneErrorMessage);
    });

    function formatPhoneNumber(phoneField) {
        let value = phoneField.value.replace(/[^0-9]/g, '');
        if (value.length > 3 && value.length <= 6) {
            value = `${value.slice(0, 3)}-${value.slice(3)}`;
        } else if (value.length > 6 && value.length <= 8) {
            value = `${value.slice(0, 3)}-${value.slice(3, 6)}-${value.slice(6)}`;
        } else if (value.length > 8) {
            value = `${value.slice(0, 3)}-${value.slice(3, 6)}-${value.slice(6, 8)}-${value.slice(8)}`;
        }
        phoneField.value = value;
    }

    function validatePhoneInput(phoneField, charCount, errorMessage) {
        const value = phoneField.value.replace(/-/g, '');
        const length = value.length;
        charCount.textContent = `${length}/10`;

        if (length === 0) {
            phoneField.classList.add('border-gray-300');
            phoneField.classList.remove('border-red-500');
            errorMessage.style.display = 'none';
        } else if (!value.startsWith('5')) {
            errorMessage.textContent = 'Telefon numarasi 5 ile baslamalidir.';
            phoneField.classList.add('border-red-500');
            phoneField.classList.remove('border-gray-300');
            errorMessage.style.display = 'block';
            setTimeout(() => errorMessage.style.display = 'none', 4000);
        } else if (length !== 10) {
            errorMessage.textContent = '10 karakter girilmelidir.';
            phoneField.classList.add('border-red-500');
            phoneField.classList.remove('border-gray-300');
            errorMessage.style.display = 'block';
            setTimeout(() => errorMessage.style.display = 'none', 4000);
        } else {
            phoneField.classList.add('border-gray-300');
            phoneField.classList.remove('border-red-500');
            errorMessage.style.display = 'none';
        }
    }
    function autocomplete(input, emailProviders) {
        let currentFocus;
        input.addEventListener("input", function () {
            let val = this.value;
            // Boþluk ve büyük harfleri engelle
            val = val.replace(/\s/g, '').toLowerCase();
            this.value = val;

            closeAllLists();
            if (!val) return false;
            currentFocus = -1;
            let a = document.createElement("DIV");
            a.setAttribute("id", this.id + "autocomplete-list");
            a.setAttribute("class", "autocomplete-items");
            this.parentNode.appendChild(a);
            for (let i = 0; i < emailProviders.length; i++) {
                let provider = emailProviders[i];
                if (val.indexOf('@') === -1) {
                    let b = document.createElement("DIV");
                    b.innerHTML = "<strong>" + val + "@" + provider + "</strong>";
                    b.innerHTML += "<input type='hidden' value='" + val + "@" + provider + "'>";
                    b.addEventListener("click", function () {
                        input.value = this.getElementsByTagName("input")[0].value;
                        closeAllLists();
                    });
                    a.appendChild(b);
                } else {
                    let prefix = val.split('@')[0];
                    if (provider.indexOf(val.split('@')[1]) === 0) {
                        let b = document.createElement("DIV");
                        b.innerHTML = "<strong>" + prefix + "@" + provider + "</strong>";
                        b.innerHTML += "<input type='hidden' value='" + prefix + "@" + provider + "'>";
                        b.addEventListener("click", function () {
                            input.value = this.getElementsByTagName("input")[0].value;
                            closeAllLists();
                        });
                        a.appendChild(b);
                    }
                }
            }
        });

        input.addEventListener("keydown", function (e) {
            let x = document.getElementById(this.id + "autocomplete-list");
            if (x) x = x.getElementsByTagName("div");
            if (e.keyCode === 40) {
                currentFocus++;
                addActive(x);
            } else if (e.keyCode === 38) {
                currentFocus--;
                addActive(x);
            } else if (e.keyCode === 13) {
                e.preventDefault();
                if (currentFocus > -1) {
                    if (x) x[currentFocus].click();
                }
            }
        });

        function addActive(x) {
            if (!x) return false;
            removeActive(x);
            if (currentFocus >= x.length) currentFocus = 0;
            if (currentFocus < 0) currentFocus = (x.length - 1);
            x[currentFocus].classList.add("autocomplete-active");
        }

        function removeActive(x) {
            for (let i = 0; i < x.length; i++) {
                x[i].classList.remove("autocomplete-active");
            }
        }

        function closeAllLists(elmnt) {
            const x = document.getElementsByClassName("autocomplete-items");
            for (let i = 0; i < x.length; i++) {
                if (elmnt !== x[i] && elmnt !== input) {
                    x[i].parentNode.removeChild(x[i]);
                }
            }
        }

        document.addEventListener("click", function (e) {
            closeAllLists(e.target);
        });
    }

    const emailField = document.getElementById('add-address-email');
    const emailProviders = [
        "hotmail.com",
        "gmail.com",
        "yahoo.com",
        "icloud.com",
        "outlook.com",
        "mail.com",
        "yandex.com"
    ];

    emailField.addEventListener("input", function () {
        let currentValue = emailField.value.split('@')[0];
        autocomplete(emailField, emailProviders);
    });

    function closeAllLists(elmnt) {
        const x = document.getElementsByClassName("autocomplete-items");
        for (let i = 0; i < x.length; i++) {
            if (elmnt !== x[i] && elmnt !== emailField) {
                x[i].parentNode.removeChild(x[i]);
            }
        }
    }

    //iptal button
    var cancelButton = document.getElementById('editAddressCancelButton');
    if (cancelButton) {
        cancelButton.addEventListener('click', function (event) {
            event.preventDefault();
            var referrer = document.referrer;
            var currentUrl = window.location.href;
            if (referrer.includes('/account/addresses') && currentUrl.includes('/edit')) {
                window.location.href = referrer;
            } else if (currentUrl.includes('/account/addresses/')) {
                var addressId = currentUrl.split('/').pop(); 
                window.location.href = '/account/addresses/' + addressId;
            } else {
                window.location.href = '/account/addresses';
            }
        });
    }

});
