namespace Api

module UseCases =

    module Registration =

        let start =
            Validation.validateAll
            >!> Validation.normalizeEmail
            >=> RegistrationService.tryConfirmEmail
            >=> Database.findByEmailRegistration
            >=> RegistrationService.setActivationCode
            >=> Database.persistRegistration
            >=> Notification.sendActivationEmail