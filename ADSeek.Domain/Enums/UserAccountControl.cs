using System;

namespace ADSeek.Domain.Enums
{
    [Flags]
    public enum UserAccountControl
    {
        /// <summary>
        /// Будет запущен сценарий входа
        /// </summary>
        Script = 0x0001,

        /// <summary>
        /// Учетная запись отключена
        /// </summary>
        Disabled = 0x0002,

        /// <summary>
        /// Требуется домашняя папка
        /// </summary>
        HomeDirectoryRequired = 0x0008,

        /// <summary>
        /// Учетная запись временно заблокирована
        /// </summary>
        Lockout = 0x0010,

        /// <summary>
        /// Для входа под учетной записью не требуется пароль
        /// </summary>
        PasswordNotRequired = 0x0020,

        /// <summary>
        /// Нельзя сменить пароль для учетной записи
        /// </summary>
        PasswordCantChange = 0x0040,

        /// <summary>
        /// Хранение пароля с обратимым шифрованием
        /// </summary>
        EncryptedTextPasswordAllowed = 0x0080,

        /// <summary>
        /// Учетная запись для пользователей, основная учетная запись которых находится в другом домене
        /// </summary>
        TempDuplicateAccount = 0x0100,

        /// <summary>
        /// Тип учетной записи по умолчанию (стандартный пользователь)
        /// </summary>
        NormalAccount = 0x0200,

        /// <summary>
        /// Разрешение на доверие учетной записи для системного домена, который доверяет другим доменам
        /// </summary>
        InterdomainTrustAccount = 0x0800,

        /// <summary>
        /// Учетная запись для компьютера
        /// </summary>
        WorkstationTrustAccount = 0x1000,

        /// <summary>
        /// Учетная запись компьютера для контроллера домена, который является членом этого домена
        /// </summary>
        ServerTrustAccount = 0x2000,

        /// <summary>
        /// Срок действия пароля не ограничен
        /// </summary>
        DontExpirePassword = 0x10000,

        /// <summary>
        /// Учетная запись MNS
        /// </summary>
        MNSLogonAccount = 0x20000,

        /// <summary>
        /// Для входа в учетную запись необходима смарт-карта
        /// </summary>
        SmartcardForLogonRequired = 0x40000,

        /// <summary>
        /// Учетная запись, под которой выполняется служба, доверяется для делегирования Kerberos
        /// </summary>
        TrustedForDelegation = 0x80000,

        /// <summary>
        /// Учетная запись важна и не может быть делегирована
        /// </summary>
        CantBeDelegated = 0x100000,

        /// <summary>
        /// Использовать типы шифрования Kerberos DES для данного аккаунта
        /// </summary>
        UseDESEncryptionTypes = 0x200000,

        /// <summary>
        /// Учетная запись не требует предварительной проверки подлинности Kerberos для входа в систему
        /// </summary>
        DontRequiredPreAuth = 0x400000,

        /// <summary>
        /// Срок действия пароля истек
        /// </summary>
        PasswordExpired = 0x800000,

        /// <summary>
        /// Учетная запись включена для делегирования
        /// </summary>
        TrustedToAuthForDelegation = 0x1000000,

        /// <summary>
        /// Учетная запись является контроллером домена только для чтения (RODC)
        /// </summary>
        PartialSecretsAccount = 0x2000000
    }
}